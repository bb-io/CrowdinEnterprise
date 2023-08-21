using Apps.CrowdinEnterprise.Api;
using Apps.CrowdinEnterprise.Api.RestSharp;
using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Models.Response;
using Apps.CrowdinEnterprise.Utils;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Crowdin.Api;
using Crowdin.Api.Webhooks;
using Crowdin.Api.Webhooks.Organization;
using RestSharp;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Base;

public abstract class OrganizationWebhookHandler : IWebhookEventHandler
{
    protected abstract EnterpriseOrgEventType SubscriptionEvent { get; }

    public async Task SubscribeAsync(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        Dictionary<string, string> values)
    {
        var client = new CrowdinEnterpriseClient(creds.ToArray());

        var request = new EnterpriseAddWebhookRequest()
        {
            Name = $"BlackBird-{SubscriptionEvent}-{Guid.NewGuid()}",
            Url = values["payloadUrl"],
            RequestType = RequestType.POST,
            Events = new List<EnterpriseOrgEventType> { SubscriptionEvent }
        };

        try
        {
            await client.OrganizationWebhooks.AddWebhook(request);
        }
        catch (Exception)
        {
            // SDK deserializes response wrong, but the request itself is successful
        }
    }

    public async Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> creds,
        Dictionary<string, string> values)
    {
        var client = new CrowdinEnterpriseClient(creds.ToArray());

        var allWebhooks = await GetAllWebhooks(creds);
        var webhookToDelete = allWebhooks.FirstOrDefault(x => x.Data.Url == values["payloadUrl"]);

        if (webhookToDelete is null)
            return;

        await client.OrganizationWebhooks.DeleteWebhook(webhookToDelete.Data.Id);
    }

    public Task<List<DataResponse<WebhookEntity>>> GetAllWebhooks(
        IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var endpoint = "/webhooks";
        var client = new CrowdinEnterpriseRestClient(creds);
        
        return Paginator.Paginate(async (lim, offset) =>
        {
            var source = $"{endpoint}?limit={lim}&offset={offset}";
            var request = new CrowdinEnterpriseRestRequest(source, Method.Get, creds);

            var response = await client.ExecuteAsync<ResponseList<DataResponse<WebhookEntity>>>(request);
            return response.Data;
        });
    }
}