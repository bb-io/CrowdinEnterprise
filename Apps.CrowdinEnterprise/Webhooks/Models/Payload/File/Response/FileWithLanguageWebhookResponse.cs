using Apps.CrowdinEnterprise.Webhooks.Models.Payload.File.Wrappers;
using Blackbird.Applications.Sdk.Common;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.File.Response;

public class FileWithLanguageWebhookResponse : CrowdinWebhookResponse<FileWithLanguageWrapper>
{
    public Apps.CrowdinEnterprise.Models.Entities.FileEntity File { get; set; }
    [Display("Target language ID")] public string LanguageId { get; set; }


    public override void ConfigureResponse(FileWithLanguageWrapper wrapper)
    {
        File = new(wrapper.File);
        File.ProjectId = wrapper.File.Project.Id.ToString();

        LanguageId = wrapper.TargetLanguage.Id;
    }
}