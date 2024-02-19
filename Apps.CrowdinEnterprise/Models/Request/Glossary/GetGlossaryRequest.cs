using Blackbird.Applications.Sdk.Common;

namespace Apps.CrowdinEnterprise.Models.Request.Glossary;

public class GetGlossaryRequest
{
    [Display("Glossary ID")]
    public string GlossaryId { get; set; }
}