using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.File.Wrappers;

namespace Apps.CrowdinEnterprise.Webhooks.Models.Payload.File.Response;

public class FileWithUserWebhookResponse : CrowdinWebhookResponse<FileWithUserWrapper>
{
    public FileEntity File { get; set; }
    public UserEntity User { get; set; }

    public override void ConfigureResponse(FileWithUserWrapper wrapper)
    {
        File = new(wrapper.File);
        File.ProjectId = wrapper.File.Project.Id.ToString();

        User = new(wrapper.User);
    }
}