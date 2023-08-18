using Apps.CrowdinEnterprise.Webhooks.Handlers.Base;
using Crowdin.Api.Webhooks.Organization;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Organization;

public class GroupCreatedHandler : OrganizationWebhookHandler
{
    protected override EnterpriseOrgEventType SubscriptionEvent => EnterpriseOrgEventType.GroupCreated;
}