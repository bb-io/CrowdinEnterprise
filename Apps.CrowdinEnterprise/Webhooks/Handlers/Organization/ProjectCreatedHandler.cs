using Apps.CrowdinEnterprise.Webhooks.Handlers.Base;
using Crowdin.Api.Webhooks.Organization;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Organization;

public class ProjectCreatedHandler : OrganizationWebhookHandler
{
    protected override EnterpriseOrgEventType SubscriptionEvent => EnterpriseOrgEventType.ProjectCreated;
}