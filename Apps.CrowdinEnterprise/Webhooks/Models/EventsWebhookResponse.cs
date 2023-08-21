namespace Apps.CrowdinEnterprise.Webhooks.Models;

public class EventsWebhookResponse<T>
{
    public List<T> Events { get; set; }
}