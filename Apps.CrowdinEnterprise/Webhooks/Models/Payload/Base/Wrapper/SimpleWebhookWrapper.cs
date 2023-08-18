using Newtonsoft.Json;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.Base.Wrapper;

public class SimpleWebhookWrapper
{
    public string Project { get; set; }

    [JsonProperty("project_id")] public string ProjectId { get; set; }
}