using Apps.CrowdinEnterprise.Webhooks.Handlers.Base;
using Apps.CrowdinEnterprise.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Crowdin.Api.Webhooks;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Project.Task;

public class TaskAddedHandler : ProjectWebhookHandler
{
    protected override EventType SubscriptionEvent => EventType.TaskAdded;

    public TaskAddedHandler([WebhookParameter] ProjectWebhookInput input) : base(input)
    {
    }
}