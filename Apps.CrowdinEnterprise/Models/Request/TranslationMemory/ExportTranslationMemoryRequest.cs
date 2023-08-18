using Apps.CrowdinEnterprise.DataSourceHandlers;
using Apps.CrowdinEnterprise.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Models.Request.TranslationMemory;

public class ExportTranslationMemoryRequest
{
    [Display("Source language")]
    [DataSource(typeof(LanguagesDataHandler))]
    public string? SourceLanguageId { get; set; }

    [Display("Target language")]
    [DataSource(typeof(LanguagesDataHandler))]
    public string? TargetLanguageId { get; set; }

    [DataSource(typeof(TmFileFormatHandler))]
    public string? Format { get; set; }
}