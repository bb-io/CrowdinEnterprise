using Apps.CrowdinEnterprise.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Models.Request.Glossary;

public class GetGlossaryRequest
{
    [Display("Glossary ID"), DataSource(typeof(GlossaryDataHandler))]
    public string GlossaryId { get; set; }
}