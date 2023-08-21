using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.String.Wrappers;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.String.Response;

public class SourceStringWebhookResponse : CrowdinWebhookResponse<EventsWebhookResponse<SourceStringWrapper>>
{
    public StringWebhookResponseEntity String { get; set; }
    public UserEntity User { get; set; }

    public override void ConfigureResponse(EventsWebhookResponse<SourceStringWrapper> wrapper)
    {
        var eventPayload = wrapper.Events.First();
        
        String = new(eventPayload.String);
        User = new(eventPayload.User);
    }
}