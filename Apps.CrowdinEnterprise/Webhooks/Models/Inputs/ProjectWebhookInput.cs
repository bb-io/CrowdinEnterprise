using Apps.CrowdinEnterprise.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Inputs;

public class ProjectWebhookInput
{
    [Display("Project")]
    [DataSource(typeof(ProjectDataHandler))]
    public string ProjectId { get; set; }
}