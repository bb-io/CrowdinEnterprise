using Apps.CrowdinEnterprise.Actions.Base;
using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Models.Request.Directory;
using Apps.CrowdinEnterprise.Models.Request.Project;
using Apps.CrowdinEnterprise.Models.Response.Directory;
using Apps.CrowdinEnterprise.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Parsers;
using Crowdin.Api.SourceFiles;
using Directory = Crowdin.Api.SourceFiles.Directory;

namespace Apps.CrowdinEnterprise.Actions;

[ActionList]
public class DirectoryActions : CrowdinActions
{
    public DirectoryActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("List directories", Description = "List all directories")]
    public async Task<ListDirectoriesResponse> ListDirectories(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] ListDirectoriesRequest input)
    {
        var items = await Paginator.Paginate((lim, offset)
            =>
        {
            var request = new DirectoriesListParams
            {
                Limit = lim,
                Offset = offset,
                BranchId = IntParser.Parse(input.BranchId, nameof(input.BranchId)),
                DirectoryId = IntParser.Parse(input.DirectoryId, nameof(input.DirectoryId)),
                Filter = input.Name,
            };
            return Client.SourceFiles.ListDirectories(int.Parse(project.ProjectId), request);
        });

        var result = items.Select(x => new DirectoryEntity(x)).ToArray();
        return new(result);
    }

    [Action("Add directory", Description = "Add a new directory")]
    public async Task<DirectoryEntity> AddDirectory(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] AddNewDirectoryRequest input)
    {
        var folders = input.Path.Trim('/').Split("/");

        if (folders.Length == 1)
        {
            var directory = await CreateSingleDirectory(project, input);
            return new(directory);
        }

        var allDirs = await ListDirectories(project, new());

        string? parentFolderId = input.DirectoryId;
        Directory? response = default;

        foreach (var folder in folders)
        {
            var folderId = allDirs.Directories
                .FirstOrDefault(x => x.Name == folder && x.DirectoryId == (parentFolderId ?? "0"))?.Id;

            if (folderId is not null)
            {
                parentFolderId = folderId;
                continue;
            }

            var request = new AddNewDirectoryRequest
            {
                Path = folder,
                Title = input.Title,
                DirectoryId = parentFolderId,
            };

            response = await CreateSingleDirectory(project, request);
            parentFolderId = response.Id.ToString();
        }

        if (response is null)
            return await GetDirectoryById(project, new()
            {
                DirectoryId = parentFolderId
            });

        return new(response);
    }

    [Action("Get directory by path", Description = "Get a specific directory by path")]
    public async Task<DirectoryEntity> GetDirectoryByPath(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] GetDirectoryByPathRequest input)
    {
        var purePath = input.PathContainsFile is true
            ? input.Path.Replace(Path.GetFileName(input.Path), string.Empty)
            : input.Path;
        var folders = purePath.Trim('/').Split("/");

        var parentFolderId = "0";
        Directory? response = default;

        foreach (var folder in folders)
        {
            var allDirs = await ListDirectories(project, new()
            {
                DirectoryId = parentFolderId
            });

            parentFolderId = allDirs.Directories
                .FirstOrDefault(x => x.Name == folder && x.DirectoryId == parentFolderId)?.Id;

            if (parentFolderId is null)
                return new()
                {
                    Id = null,
                    ProjectId = project.ProjectId,
                    Name = "Root"
                };
        }

        return await GetDirectoryById(project, new()
        {
            DirectoryId = parentFolderId
        });
    }

    [Action("Get directory by ID", Description = "Get a specific directory by ID")]
    public async Task<DirectoryEntity> GetDirectoryById(
        [ActionParameter] ProjectRequest project,
        [ActionParameter] DirectoryRequest directory)
    {
        var projectId = IntParser.Parse(project.ProjectId, nameof(project.ProjectId))!.Value;
        var directoryId = IntParser.Parse(directory.DirectoryId, nameof(directory.DirectoryId))!.Value;

        var response = await Client.SourceFiles.GetDirectory(projectId, directoryId);
        return new(response);
    }

    private Task<Directory> CreateSingleDirectory(ProjectRequest proj, AddNewDirectoryRequest input)
    {
        var request = new AddDirectoryRequest
        {
            Name = input.Path,
            Title = input.Title,
            BranchId = IntParser.Parse(input.BranchId, nameof(input.BranchId)),
            DirectoryId = IntParser.Parse(input.DirectoryId, nameof(input.DirectoryId)),
        };

        return Client.SourceFiles.AddDirectory(int.Parse(proj.ProjectId), request);
    }
}