using Crowdin.Api.StringTranslations;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.String.Wrappers;

public class SourceStringWrapper
{
    public StringPayload String { get; set; }
    public User User { get; set; }
}