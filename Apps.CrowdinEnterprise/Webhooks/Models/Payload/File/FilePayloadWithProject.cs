using Crowdin.Api.ProjectsGroups;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.File;

public class FilePayloadWithProject : FilePayload
{
    public EnterpriseProject Project { get; set; }
}