using Apps.CrowdinEnterprise.Webhooks.Handlers.Base;
using Apps.CrowdinEnterprise.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Crowdin.Api.Webhooks;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Project.File;

public class FileDeletedHandler : ProjectWebhookHandler
{
    protected override EventType SubscriptionEvent => EventType.FileDeleted;

    public FileDeletedHandler([WebhookParameter] ProjectWebhookInput input) : base(input)
    {
    }
}