using Apps.CrowdinEnterprise.Models.Request.Project;
using Blackbird.Applications.Sdk.Common;

namespace Apps.CrowdinEnterprise.Models.Request.Translation;

public class GetTranslationRequest : ProjectRequest
{
    [Display("Translation ID")] public string TranslationId { get; set; }
    [Display("Denormalize placeholders")] public bool? DenormalizePlaceholders { get; set; }
}