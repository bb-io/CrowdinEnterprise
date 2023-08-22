using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.String;
using Blackbird.Applications.Sdk.Common;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.Translation.Response;

public class NewTranslationWebhookResponse : OldTranslationWebhookResponse
{
    public UserEntity User { get; set; }
    
    [Display("Language ID")]
    public string LanguageId { get; set; }
    
    [Display("Source string")]
    public StringWebhookResponseEntity SourceString { get; set; }
    
    public NewTranslationWebhookResponse(NewTranslationPayload payload) : base(payload)
    {
        User = new(payload.User);
        LanguageId = payload.TargetLanguage.Id;
        SourceString = new(payload.String);
    }
}