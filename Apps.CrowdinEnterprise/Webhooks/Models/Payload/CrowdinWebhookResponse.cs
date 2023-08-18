namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload;

public abstract class CrowdinWebhookResponse<T>
{
    public abstract void ConfigureResponse(T wrapper);
}