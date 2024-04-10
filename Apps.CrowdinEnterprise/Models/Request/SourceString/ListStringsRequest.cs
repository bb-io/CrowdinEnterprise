using Apps.CrowdinEnterprise.DataSourceHandlers.EnumHandlers;
using Apps.CrowdinEnterprise.Models.Request.Project;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.CrowdinEnterprise.Models.Request.SourceString;

public class ListStringsRequest : ProjectRequest
{
    [Display("Denormalize placeholders")] public bool? DenormalizePlaceholders { get; set; }

    [Display("Label IDs")] public string? LabelIds { get; set; }

    [Display("File ID")] public string? FileId { get; set; }

    [Display("Branch ID")] public string? BranchId { get; set; }

    [Display("Directory ID")] public string? DirectoryId { get; set; }

    public string? CroQl { get; set; }
    public string? Filter { get; set; }
    
    [StaticDataSource(typeof(StringScopeHandler))]
    public string? Scope { get; set; }
}