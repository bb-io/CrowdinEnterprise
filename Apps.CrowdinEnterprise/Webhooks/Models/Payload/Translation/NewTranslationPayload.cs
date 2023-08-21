using Apps.CrowdinEnterprise.Webhooks.Models.Payload.String;
using Crowdin.Api.StringTranslations;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.Translation;

public class NewTranslationPayload : OldTranslationPayload
{
    public User User { get; set; }
    
    public LanguagePayload TargetLanguage { get; set; }
    
    public StringPayload String { get; set; }
}