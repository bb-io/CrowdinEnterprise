using Apps.CrowdinEnterprise.Api;
using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Models.Request.Project;
using Apps.CrowdinEnterprise.Models.Response.File;
using Apps.CrowdinEnterprise.Models.Response.Project;
using Apps.CrowdinEnterprise.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.Sdk.Utils.Models;
using Blackbird.Applications.Sdk.Utils.Parsers;
using Blackbird.Applications.Sdk.Utils.Utilities;
using Crowdin.Api.ProjectsGroups;
using Crowdin.Api.Translations;

namespace Apps.CrowdinEnterprise.Actions;

[ActionList]
public class ProjectActions : BaseInvocable
{
    private AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    private readonly IFileManagementClient _fileManagementClient;

    public ProjectActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(
        invocationContext)
    {
        _fileManagementClient = fileManagementClient;
    }

    [Action("List projects", Description = "List all projects")]
    public async Task<ListProjectsResponse> ListProjects([ActionParameter] ListProjectsRequest input)
    {
        var userId = IntParser.Parse(input.UserId, nameof(input.UserId));
        var groupId = IntParser.Parse(input.GroupID, nameof(input.GroupID));

        var client = new CrowdinEnterpriseClient(Creds);

        var items = await Paginator.Paginate((lim, offset)
            => client.ProjectsGroups.ListProjects<EnterpriseProject>(userId, groupId, input.HasManagerAccess ?? false,
                lim, offset));

        var projects = items.Select(x => new ProjectEntity(x)).ToArray();

        return new(projects);
    }

    [Action("Get project", Description = "Get specific project")]
    public async Task<ProjectEntity> GetProject([ActionParameter] ProjectRequest project)
    {
        var intProjectId = IntParser.Parse(project.ProjectId, nameof(project.ProjectId));

        var client = new CrowdinEnterpriseClient(Creds);

        var response = await client.ProjectsGroups.GetProject<EnterpriseProject>(intProjectId!.Value);
        return new(response);
    }

    [Action("Add project", Description = "Add new project")]
    public async Task<ProjectEntity> AddProject([ActionParameter] AddNewProjectRequest input)
    {
        var client = new CrowdinEnterpriseClient(Creds);

        var request = new EnterpriseProjectForm
        {
            Name = input.Name,
            SourceLanguageId = input.SourceLanguageId,
            TargetLanguageIds = input.TargetLanguageIds?.ToList(),
            TemplateId = IntParser.Parse(input.TemplateId, nameof(input.TemplateId)),
            GroupId = IntParser.Parse(input.GroupId, nameof(input.GroupId)),
            VendorId = IntParser.Parse(input.VendorId, nameof(input.VendorId)),
            MtEngineId = IntParser.Parse(input.MtEngineId, nameof(input.MtEngineId)),
            TranslateDuplicates = EnumParser.Parse<DupTranslateAction>(input.TranslateDuplicates,
                nameof(input.TranslateDuplicates)),
            TagsDetection = EnumParser.Parse<TagsDetectionAction>(input.TagsDetection, nameof(input.TagsDetection)),
            DelayedWorkflowStart = input.DelayedWorkflowStart,
            ExportWithMinApprovalsCount = input.ExportWithMinApprovalsCount,
            NormalizePlaceholder = input.NormalizePlaceholder,
            SaveMetaInfoInSource = input.SaveMetaInfoInSource,
            CustomQaCheckIds = input.CustomQaCheckIds?.Select(x => IntParser.Parse(x, "QA check ID") ?? default)
                .ToList(),
            GlossaryAccess = input.GlossaryAccess,
            Description = input.Description,
            IsMtAllowed = input.IsMtAllowed,
            AutoSubstitution = input.AutoSubstitution,
            AutoTranslateDialects = input.AutoTranslateDialects,
            PublicDownloads = input.PublicDownloads,
            HiddenStringsProofreadersAccess = input.HiddenStringsProofreadersAccess,
            SkipUntranslatedStrings = input.SkipUntranslatedStrings,
            SkipUntranslatedFiles = input.SkipUntranslatedFiles,
            InContext = input.InContext,
            InContextProcessHiddenStrings = input.InContextProcessHiddenStrings,
            InContextPseudoLanguageId = input.InContextPseudoLanguageId,
            QaCheckIsActive = input.QaCheckIsActive,
            NotificationSettings = new()
            {
                ManagerLanguageCompleted = input.ManagerLanguageCompleted,
                TranslatorNewStrings = input.TranslatorNewStrings,
                ManagerNewStrings = input.ManagerNewStrings,
            }
        };

        var response = await client.ProjectsGroups.AddProject<EnterpriseProject>(request);
        return new(response);
    }

    [Action("Delete project", Description = "Delete specific project")]
    public Task DeleteProject([ActionParameter] ProjectRequest project)
    {
        var intProjectId = IntParser.Parse(project.ProjectId, nameof(project.ProjectId));

        var client = new CrowdinEnterpriseClient(Creds);

        return client.ProjectsGroups.DeleteProject(intProjectId!.Value);
    }

    [Action("Build project", Description = "Build project translation")]
    public async Task<ProjectBuildEntity> BuildProject([ActionParameter] ProjectRequest project,
        [ActionParameter] BuildProjectRequest input)
    {
        var intProjectId = IntParser.Parse(project.ProjectId, nameof(project.ProjectId));

        var client = new CrowdinEnterpriseClient(Creds);

        var response = await client.Translations.BuildProjectTranslation(intProjectId!.Value,
            new EnterpriseTranslationCreateProjectBuildForm()
            {
                BranchId = IntParser.Parse(input.BranchId, nameof(input.BranchId)),
                TargetLanguageIds = input.TargetLanguageIds?.ToList(),
                SkipUntranslatedStrings = input.SkipUntranslatedStrings,
                SkipUntranslatedFiles = input.SkipUntranslatedFiles,
                ExportWithMinApprovalsCount = input.ExportWithMinApprovalsCount
            });

        return new(response);
    }

    [Action("Download translations as ZIP", Description = "Download project translations as ZIP")]
    public async Task<DownloadFileResponse> DownloadTranslationsAsZip(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] BuildRequest build)
    {
        var intProjectId = IntParser.Parse(project.ProjectId, nameof(project.ProjectId))!.Value;
        var intBuildId = IntParser.Parse(build.BuildId, nameof(build.BuildId))!.Value;

        var client = new CrowdinEnterpriseClient(Creds);

        var response = await client.Translations.DownloadProjectTranslations(intProjectId, intBuildId);

        if (response.Link is null)
            throw new("Project build is in progress, you can't download the data now");

        var file = await FileDownloader.DownloadFileBytes(response.Link.Url);
        file.Name = $"{project.ProjectId}";
        
        var fileReference = await _fileManagementClient.UploadAsync(file.FileStream, file.ContentType, file.Name);
        return new(fileReference);
    }

    [Action("Download translations", Description = "Download project translations")]
    public async Task<DownloadFilesResponse> DownloadTranslations(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] BuildRequest build)
    {
        var filesArchive = await DownloadTranslationsAsZip(project, build);

        var zipFile = await _fileManagementClient.DownloadAsync(filesArchive.File);
        var zipBytes = await zipFile.GetByteData();
        var files = await zipFile.GetFilesFromZip();

        var result = files.Where(x => x.FileStream.Length > 0).ToArray();

        // Cleaning files path from the root folder of the archive
        result.ToList().ForEach(x =>
            x.Path = string.Join('/', x.Path.Split("/").Skip(1)));

        var blackbirdFiles = result.Select(x =>
        {
            var contentType = MimeTypes.GetMimeType(x.UploadName);
            return new BlackbirdFile(x.FileStream, x.UploadName, contentType);
        }).ToArray();
        return new(blackbirdFiles);
    }
}