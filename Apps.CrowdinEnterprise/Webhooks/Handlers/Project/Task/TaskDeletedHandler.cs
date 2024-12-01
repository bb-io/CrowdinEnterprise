using Apps.CrowdinEnterprise.Webhooks.Handlers.Base;
using Apps.CrowdinEnterprise.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Crowdin.Api.Webhooks;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Project.Task;

public class TaskDeletedHandler : ProjectWebhookHandler
{
    protected override EventType SubscriptionEvent => EventType.TaskDeleted;

    public TaskDeletedHandler([WebhookParameter(true)] ProjectWebhookInput input) : base(input)
    {
    }
}