﻿using Crowdin.Api.ProjectsGroups;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.Project.Wrappers.Base;

public class ProjectWrapper
{
    public EnterpriseProject Project { get; set; }
}