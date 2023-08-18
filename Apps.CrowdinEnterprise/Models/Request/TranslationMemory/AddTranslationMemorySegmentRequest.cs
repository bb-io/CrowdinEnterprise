using Apps.CrowdinEnterprise.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Models.Request.TranslationMemory;

public class AddTranslationMemorySegmentRequest : TranslationMemoryRequest
{
    [Display("Language")]
    [DataSource(typeof(LanguagesDataHandler))]
    public string LanguageId { get; set; }
    
    public string Text { get; set; }
}