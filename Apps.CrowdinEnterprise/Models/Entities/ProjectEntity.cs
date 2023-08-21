using Blackbird.Applications.Sdk.Common;
using Crowdin.Api.ProjectsGroups;

namespace Apps.CrowdinEnterprise.Models.Entities;

public class ProjectEntity
{
    [Display("ID")] public string Id { get; set; }

    [Display("User ID")] public string UserId { get; set; }

    [Display("Group ID")] public string GroupId { get; set; }

    [Display("Workflow ID")] public string WorkflowId { get; set; }

    [Display("Source language ID")] public string SourceLanguageId { get; set; }

    [Display("Target language IDs")] public IEnumerable<string> TargetLanguageIds { get; set; }

    [Display("Target language ID")] public string? TargetLanguageId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string? Background { get; set; }

    [Display("Is external")] public bool IsExternal { get; set; }

    [Display("Has crowd sourcing")] public bool HasCrowdSourcing { get; set; }

    [Display("External type")] public string? ExternalType { get; set; }
    public string Logo { get; set; }

    [Display("Created at")] public DateTime CreatedAt { get; set; }

    [Display("Last activity")] public DateTime? LastActivity { get; set; }

    public ProjectEntity(EnterpriseProject project)
    {
        Id = project.Id.ToString();
        UserId = project.UserId.ToString();
        GroupId = project.GroupId.ToString();
        WorkflowId = project.WorkflowId.ToString();
        SourceLanguageId = project.SourceLanguageId;
        TargetLanguageIds = project.TargetLanguageIds;
        TargetLanguageId = project.TargetLanguageIds.FirstOrDefault();
        Name = project.Name;
        Description = project.Description;
        Background = project.Background;
        IsExternal = project.IsExternal;
        HasCrowdSourcing = project.HasCrowdSourcing;
        ExternalType = project.ExternalType?.ToString();
        Logo = project.Logo;
        CreatedAt = project.CreatedAt.DateTime;
        LastActivity = project.LastActivity?.DateTime;
    }
}