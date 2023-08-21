using Apps.CrowdinEnterprise.DataSourceHandlers;
using Apps.CrowdinEnterprise.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Models.Request.Project;

public class AddNewProjectRequest
{
    public string Name { get; set; }

    [Display("Source language")]
    [DataSource(typeof(LanguagesDataHandler))]
    public string SourceLanguageId { get; set; }

    [Display("Target language IDs")] public IEnumerable<string>? TargetLanguageIds { get; set; }
    
    [Display("Template ID")] public string? TemplateId { get; set; }
    
    [Display("Group ID")] 
    [DataSource(typeof(ProjectGroupDataHandler))]
    public string? GroupId { get; set; }
    
    [Display("Vendor ID")] public string? VendorId { get; set; }
    [Display("MT Engine ID")] public string? MtEngineId { get; set; }
    
    public string? Description { get; set; }

    [Display("Is MT allowed")] public bool? IsMtAllowed { get; set; }

    [Display("Auto substitution")] public bool? AutoSubstitution { get; set; }

    [Display("Auto translate dialects")] public bool? AutoTranslateDialects { get; set; }

    [Display("Public downloads")] public bool? PublicDownloads { get; set; }

    [Display("Hidden strings proofreaders access")]
    public bool? HiddenStringsProofreadersAccess { get; set; }

    [Display("Skip untranslated strings")] public bool? SkipUntranslatedStrings { get; set; }

    [Display("Skip untranslated files")] public bool? SkipUntranslatedFiles { get; set; }

    [Display("In context")] public bool? InContext { get; set; }

    [Display("In context process hidden strings")]
    public bool? InContextProcessHiddenStrings { get; set; }

    [Display("In context pseudo language ID")]
    public string? InContextPseudoLanguageId { get; set; }

    [Display("QA check is active")] public bool? QaCheckIsActive { get; set; }
    
    [Display("Translate duplicates")]
    [DataSource(typeof(TranslateDuplicatesDataHandler))]
    public string? TranslateDuplicates { get; set; }

    [Display("Tags detection")]
    [DataSource(typeof(TagsDetectionDataHandler))]
    public string? TagsDetection { get; set; }

    [Display("Delayed workflow start")]
    public bool? DelayedWorkflowStart { get; set; }

    [Display("Export with min pprovals count")]
    public int? ExportWithMinApprovalsCount { get; set; }

    [Display("Normalize placeholder")]
    public bool? NormalizePlaceholder { get; set; }

    [Display("Save meta info in source")]
    public bool? SaveMetaInfoInSource { get; set; }

    [Display("Custom QA check IDs")]
    public IEnumerable<string>? CustomQaCheckIds { get; set; }

    [Display("Glossary access")]
    public bool? GlossaryAccess { get; set; }

    [Display("Manager language completed")]
    public bool? ManagerLanguageCompleted { get; set; }

    [Display("Translator new strings")]
    public bool? TranslatorNewStrings { get; set; }

    [Display("Manager new strings")]
    public bool? ManagerNewStrings { get; set; }
}