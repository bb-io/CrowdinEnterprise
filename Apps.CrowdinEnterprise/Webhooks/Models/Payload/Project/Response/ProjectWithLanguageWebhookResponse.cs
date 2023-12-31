﻿using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.Project.Wrappers;
using Blackbird.Applications.Sdk.Common;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.Project.Response;

public class ProjectWithLanguageWebhookResponse : CrowdinWebhookResponse<ProjectWithLanguageWrapper>
{
    public ProjectEntity Project { get; set; }
    [Display("Target language ID")] public string LanguageId { get; set; }

    public override void ConfigureResponse(ProjectWithLanguageWrapper wrapper)
    {
        Project = new(wrapper.Project);
        LanguageId = wrapper.TargetLanguage.Id;
    }
}