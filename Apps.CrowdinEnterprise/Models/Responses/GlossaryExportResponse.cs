using System.Text.Json.Serialization;

namespace Apps.CrowdinEnterprise.Models.Responses;

public class GlossaryExportResponse
{
    [JsonPropertyName("data")]
    public ExportGlossaryModel Data { get; set; }
}

public class ExportGlossaryModel
{
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }
}