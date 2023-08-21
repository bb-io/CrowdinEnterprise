using Blackbird.Applications.Sdk.Common;
using Crowdin.Api.SourceFiles;

namespace Apps.CrowdinEnterprise.Models.Entities;

public class ReviewedFileBuildEntity
{
    [Display("ID")]
    public string Id { get; set; }

    [Display("Project ID")]
    public string ProjectId { get; set; }

    public string Status { get; set; }

    public int Progress { get; set; }
    
    [Display("Branch ID")]
    public string BranchId { get; set; }

    [Display("Target language")]
    public string TargetLanguage { get; set; }

    public ReviewedFileBuildEntity(ReviewedStringBuild build)
    {
        Id = build.Id.ToString();
        ProjectId = build.ProjectId.ToString();
        Status = build.Status.ToString();
        Progress = build.Progress;
        BranchId = build.Attributes.BranchId.ToString();
        TargetLanguage = build.Attributes.TargetLanguageId;
    }
}