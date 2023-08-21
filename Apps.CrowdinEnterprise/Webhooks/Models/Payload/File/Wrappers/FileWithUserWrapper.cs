using Apps.CrowdinEnterprise.Webhooks.Models.Payload.File.Wrappers.Base;
using Crowdin.Api.StringTranslations;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.File.Wrappers;

public class FileWithUserWrapper : FileWrapper
{
    public User User { get; set; }
}