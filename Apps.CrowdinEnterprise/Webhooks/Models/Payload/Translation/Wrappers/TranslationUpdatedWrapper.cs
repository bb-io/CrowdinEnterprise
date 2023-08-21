namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.Translation.Wrappers;

public class TranslationUpdatedWrapper
{
    public OldTranslationPayload OldTranslation { get; set; }
    public NewTranslationPayload NewTranslation { get; set; }
}