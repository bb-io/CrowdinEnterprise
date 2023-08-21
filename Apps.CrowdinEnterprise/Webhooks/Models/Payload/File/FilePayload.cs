using Crowdin.Api.ProjectsGroups;
using Crowdin.Api.SourceFiles;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.File;

public class FilePayload : FileCollectionResource
{
    public EnterpriseProject Project { get; set; }
}