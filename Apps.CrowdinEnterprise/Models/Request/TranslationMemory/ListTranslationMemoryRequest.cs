using Apps.CrowdinEnterprise.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Models.Request.TranslationMemory;

public class ListTranslationMemoryRequest
{
    [Display("User ID")] 
    [DataSource(typeof(UserDataHandler))]
    public string? UserId { get; set; }
    
    [Display("Group ID")]
    [DataSource(typeof(ProjectGroupDataHandler))]
    public string? GroupId { get; set; }
}