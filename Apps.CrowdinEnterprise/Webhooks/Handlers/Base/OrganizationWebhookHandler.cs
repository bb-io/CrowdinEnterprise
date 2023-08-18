using Apps.CrowdinEnterprise.Api;
using Apps.CrowdinEnterprise.Utils;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Crowdin.Api.Webhooks;
using Crowdin.Api.Webhooks.Organization;
using Newtonsoft.Json;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Base;

public abstract class OrganizationWebhookHandler : IWebhookEventHandler
{
    protected abstract EnterpriseOrgEventType SubscriptionEvent { get; }

    public async Task SubscribeAsync(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        Dictionary<string, string> values)
    {
        var client = new CrowdinEnterpriseClient(creds.ToArray());
        var executor = new OrganizationWebhooksApiExecutor(client);

        var request = new EnterpriseAddWebhookRequest()
        {
            Name = $"BlackBird-{SubscriptionEvent}-{Guid.NewGuid()}",
            Url = values["payloadUrl"],
            RequestType = RequestType.POST,
            Events = new List<EnterpriseOrgEventType> { SubscriptionEvent }
        };

        try
        {
            await executor.AddWebhook(request);
        }
        catch (JsonSerializationException)
        {
            // SDK deserializes response wrong, but the request itself is successful
        }
    }

    public async Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> creds,
        Dictionary<string, string> values)
    {
        var client = new CrowdinEnterpriseClient(creds.ToArray());

        var allWebhooks = await GetAllWebhooks(client);
        var webhookToDelete = allWebhooks.FirstOrDefault(x => x.Url == values["payloadUrl"]);

        if (webhookToDelete is null)
            return;

        await client.OrganizationWebhooks.DeleteWebhook(webhookToDelete.Id);
    }

    private Task<List<OrganizationWebhookResource>> GetAllWebhooks(
        CrowdinEnterpriseClient client)
    {
        return Paginator.Paginate((lim, offset) =>
            client.OrganizationWebhooks.ListWebhooks(lim, offset));
    }
}