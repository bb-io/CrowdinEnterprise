﻿using Crowdin.Api.ProjectsGroups;
using Crowdin.Api.StringTranslations;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.Project.Wrappers;

public class GroupWrapper
{
    public Group Group { get; set; }
    public User User { get; set; }
}