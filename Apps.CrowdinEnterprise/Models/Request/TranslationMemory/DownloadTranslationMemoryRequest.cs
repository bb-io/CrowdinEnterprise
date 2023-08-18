using Blackbird.Applications.Sdk.Common;

namespace Apps.CrowdinEnterprise.Models.Request.TranslationMemory;

public class DownloadTranslationMemoryRequest : TranslationMemoryRequest
{
    [Display("Export ID")]
    public string ExportId { get; set; }
}