using Apps.CrowdinEnterprise.Api;
using Apps.CrowdinEnterprise.Api.RestSharp;
using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Models.Response;
using Apps.CrowdinEnterprise.Utils;
using Apps.CrowdinEnterprise.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Blackbird.Applications.Sdk.Utils.Parsers;
using Crowdin.Api;
using Crowdin.Api.Webhooks;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Base;

public abstract class ProjectWebhookHandler : IWebhookEventHandler
{
    protected abstract EventType SubscriptionEvent { get; }
    private int ProjectId { get; }
    private bool EnableBatchingWebhooks { get; }

    protected ProjectWebhookHandler([WebhookParameter] ProjectWebhookInput input, bool enableBatching = false)
    {
        ProjectId = IntParser.Parse(input.ProjectId, nameof(input.ProjectId))!.Value;
        EnableBatchingWebhooks = enableBatching;
    }

    public async Task SubscribeAsync(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        Dictionary<string, string> values)
    {
        var client = new CrowdinEnterpriseClient(creds.ToArray());

        var request = new AddWebhookRequest
        {
            Name = $"BlackBird-{SubscriptionEvent}-{Guid.NewGuid()}",
            Url = values["payloadUrl"],
            RequestType = RequestType.POST,
            Events = new List<EventType> { SubscriptionEvent },
            BatchingEnabled = EnableBatchingWebhooks
        };

        try
        {
            await client.Webhooks.AddWebhook(ProjectId, request);
        }
        catch (JsonSerializationException)
        {
            // SDK deserializes response wrong, but the request itself is successful
        }
    }

    public async Task UnsubscribeAsync(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        Dictionary<string, string> values)
    {
        var client = new CrowdinEnterpriseClient(creds.ToArray());
        var allWebhooks = await GetAllWebhooks(creds);

        var webhookToDelete = allWebhooks.FirstOrDefault(x => x.Data.Url == values["payloadUrl"]);

        if (webhookToDelete is null)
            return;
        
        await client.Webhooks.DeleteWebhook(ProjectId, webhookToDelete.Data.Id);
    }

    private Task<List<DataResponse<WebhookEntity>>> GetAllWebhooks(
        IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var endpoint = $"/projects/{ProjectId}/webhooks";
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