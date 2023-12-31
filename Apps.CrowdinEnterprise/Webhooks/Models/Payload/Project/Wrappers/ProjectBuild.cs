﻿using Crowdin.Api.ProjectsGroups;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.Project.Wrappers;

public class ProjectBuild
{
    public string Id { get; set; }
    public string DownloadUrl { get; set; }
    public EnterpriseProject Project { get; set; }
}