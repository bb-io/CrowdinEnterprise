using Apps.CrowdinEnterprise.Api;
using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Models.Request.File;
using Apps.CrowdinEnterprise.Models.Request.File.Base;
using Apps.CrowdinEnterprise.Models.Request.Project;
using Apps.CrowdinEnterprise.Models.Response.File;
using Apps.CrowdinEnterprise.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Parsers;
using Blackbird.Applications.Sdk.Utils.Utilities;
using Crowdin.Api;
using Crowdin.Api.SourceFiles;

namespace Apps.CrowdinEnterprise.Actions;

[ActionList]
public class FileActions : BaseInvocable
{
    private AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public FileActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("List files", Description = "List project files")]
    public async Task<ListFilesResponse> ListFiles(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] ListFilesRequest input)
    {
        var intProjectId = IntParser.Parse(project.ProjectId, nameof(project.ProjectId));
        var intBranchId = IntParser.Parse(input.BranchId, nameof(input.BranchId));
        var intDirectoryId = IntParser.Parse(input.DirectoryId, nameof(input.DirectoryId));

        var client = new CrowdinEnterpriseClient(Creds);

        var items = await Paginator.Paginate((lim, offset) =>
        {
            var request = new FilesListParams
            {
                BranchId = intBranchId,
                DirectoryId = intDirectoryId,
                Filter = input.Filter,
                Limit = lim,
                Offset = offset
            };

            return client.SourceFiles.ListFiles<FileCollectionResource>(intProjectId!.Value, request);
        });

        var files = items.Select(x => new FileEntity(x)).ToArray();
        return new(files);
    }

    [Action("Add file", Description = "Add new file")]
    public async Task<FileEntity> AddFile(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] AddNewFileRequest input)
    {
        if (input.StorageId is null && input.File is null)
            throw new("You need to specfiy one of the parameters: Storage ID or File");

        var intProjectId = IntParser.Parse(project.ProjectId, nameof(project.ProjectId));
        var intStorageId = IntParser.Parse(input.StorageId, nameof(input.StorageId));
        var intBranchId = IntParser.Parse(input.BranchId, nameof(input.BranchId));
        var intDirectoryId = IntParser.Parse(input.DirectoryId, nameof(input.DirectoryId));

        var client = new CrowdinEnterpriseClient(Creds);

        var fileName = input.Name ?? input.File?.Name;

        if (intStorageId is null)
        {
            var storage = await client.Storage
                .AddStorage(new MemoryStream(input.File!.Bytes), fileName!);
            intStorageId = storage.Id;
        }

        if (input.File is null)
        {
            var storage = await new StorageActions(InvocationContext).GetStorage(new()
            {
                StorageId = intStorageId.ToString()!
            });

            fileName = storage.FileName;
        }

        try
        {
            var request = new AddFileRequest
            {
                StorageId = intStorageId.Value,
                Name = fileName!,
                BranchId = intBranchId,
                DirectoryId = intDirectoryId,
                Title = input.Title,
                ExcludedTargetLanguages = input.ExcludedTargetLanguages?.ToList(),
                AttachLabelIds = input.AttachLabelIds?.ToList()
            };
            var file = await client.SourceFiles.AddFile(intProjectId!.Value, request);

            return new(file);
        }
        catch (CrowdinApiException ex)
        {
            if (!ex.Message.Contains("Name must be unique"))
                throw;

            var allFiles = await ListFiles(project, new());
            var fileToUpdate = allFiles.Files.First(x => x.Name == fileName);
            
            return await UpdateFile(project, new()
            {
                FileId = fileToUpdate.Id
            }, new()
            {
                StorageId = intStorageId.ToString()
            });
        }
    }
    
    [Action("Update file", Description = "Update specific file")]
    public async Task<FileEntity> UpdateFile(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] FileRequest file,
        [ActionParameter] ManageFileRequest input)
    {
        if (input.StorageId is null && input.File is null)
            throw new("You need to specfiy one of the parameters: Storage ID or File");

        var intProjectId = IntParser.Parse(project.ProjectId, nameof(project.ProjectId));
        var intStorageId = IntParser.Parse(input.StorageId, nameof(input.StorageId));
        var intFileId = IntParser.Parse(file.FileId, nameof(file.FileId));

        var client = new CrowdinEnterpriseClient(Creds);

        if (intStorageId is null)
        {
            var storage = await client.Storage
                .AddStorage(new MemoryStream(input.File!.Bytes), input.File.Name);
            intStorageId = storage.Id;
        }

        var request = new ReplaceFileRequest()
        {
            StorageId = intStorageId.Value,
        };
        var (result, _) = await client.SourceFiles.UpdateOrRestoreFile(
            intProjectId!.Value, 
            intFileId!.Value,
            request);

        return new(result);
    }

    [Action("Get file", Description = "Get specific file info")]
    public async Task<FileEntity> GetFile(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] FileRequest file)
    {
        var intProjectId = IntParser.Parse(project.ProjectId, nameof(project.ProjectId));
        var intFileId = IntParser.Parse(file.FileId, nameof(file.FileId));

        var client = new CrowdinEnterpriseClient(Creds);

        var result = await client.SourceFiles.GetFile<FileResource>(intProjectId!.Value, intFileId!.Value);
        return new(result);
    }

    [Action("Download file", Description = "Download specific file")]
    public async Task<DownloadFileResponse> DownloadFile(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] FileRequest file)
    {
        var intProjectId = IntParser.Parse(project.ProjectId, nameof(project.ProjectId));
        var intFileId = IntParser.Parse(file.FileId, nameof(file.FileId));

        var client = new CrowdinEnterpriseClient(Creds);

        var downloadLink = await client.SourceFiles.DownloadFile(intProjectId!.Value, intFileId!.Value);

        var fileContent = await FileDownloader.DownloadFileBytes(downloadLink.Url);
        var fileDetails = await GetFile(project, file);
        
        fileContent.Name = fileDetails.Name;

        return new(fileContent);
    }

    [Action("Delete file", Description = "Delete specific file")]
    public Task DeleteFile(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] FileRequest file)
    {
        var intProjectId = IntParser.Parse(project.ProjectId, nameof(project.ProjectId));
        var intFileId = IntParser.Parse(file.FileId, nameof(file.FileId));

        var client = new CrowdinEnterpriseClient(Creds);

        return client.SourceFiles.DeleteFile(intProjectId!.Value, intFileId!.Value);
    }
}