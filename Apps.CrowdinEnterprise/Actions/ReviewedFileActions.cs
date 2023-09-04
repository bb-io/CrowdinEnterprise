using System.Net.Mime;
using Apps.CrowdinEnterprise.Api;
using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Models.Request.Project;
using Apps.CrowdinEnterprise.Models.Response.File;
using Apps.CrowdinEnterprise.Models.Response.ReviewedFile;
using Apps.CrowdinEnterprise.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.Sdk.Utils.Parsers;
using Blackbird.Applications.Sdk.Utils.Utilities;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.CrowdinEnterprise.Actions;

[ActionList]
public class ReviewedFileActions : BaseInvocable
{
    private AuthenticationCredentialsProvider[] Creds 
        => InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public ReviewedFileActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }
    
    [Action("List reviewed source files builds", Description = "List all reviewed source files builds of specific project")]
    public async Task<ListReviewedFileBuildsResponse> ListFileBuilds([ActionParameter] ProjectRequest project,
        [ActionParameter] [Display("Branch ID")]
        string? branchId)
    {
        var intProjectId = IntParser.Parse(project.ProjectId, nameof(project.ProjectId))!.Value;
        var intBranchId = IntParser.Parse(branchId, nameof(branchId));

        var client = new CrowdinEnterpriseClient(Creds);

        var items = await Paginator.Paginate((lim, offset) =>
            client.SourceFiles.ListReviewedSourceFilesBuilds(intProjectId, intBranchId, lim, offset));

        var result = items.Select(x => new ReviewedFileBuildEntity(x)).ToArray();
        return new(result);
    }  
    
    [Action("Build reviewed source files", Description = "Build reviewed source files of specific project")]
    public async Task<ReviewedFileBuildEntity> BuildReviewedSourceFiles([ActionParameter] ProjectRequest project,
        [ActionParameter] [Display("Branch ID")]
        string? branchId)
    {
        var intProjectId = IntParser.Parse(project.ProjectId, nameof(project.ProjectId))!.Value;
        var intBranchId = IntParser.Parse(branchId, nameof(branchId));

        var client = new CrowdinEnterpriseClient(Creds);

        var response = await client.SourceFiles.BuildReviewedSourceFiles(intProjectId, new()
        {
            BranchId = intBranchId
        });

        return new(response);
    }
    
    [Action("Get reviewed source files build", Description = "Get specific reviewed source files build")]
    public async Task<ReviewedFileBuildEntity> GetReviewedSourceFilesBuild([ActionParameter] ProjectRequest project,
        [ActionParameter] [Display("Build ID")]
        string buildId)
    {
        var intProjectId = IntParser.Parse(project.ProjectId, nameof(project.ProjectId))!.Value;
        var intBuildId = IntParser.Parse(buildId, nameof(buildId))!.Value;

        var client = new CrowdinEnterpriseClient(Creds);

        var response = await client.SourceFiles.CheckReviewedSourceFilesBuildStatus(intProjectId, intBuildId);
        return new(response);
    }
    
    [Action("Download reviewed source files as ZIP", Description = "Download reviewed source files of specific build as ZIP")]
    public async Task<DownloadFileResponse> DownloadReviewedSourceFilesAsZip([ActionParameter] ProjectRequest project,
        [ActionParameter] [Display("Build ID")]
        string buildId)
    {
        var intProjectId = IntParser.Parse(project.ProjectId, nameof(project.ProjectId))!.Value;
        var intBuildId = IntParser.Parse(buildId, nameof(buildId))!.Value;

        var client = new CrowdinEnterpriseClient(Creds);

        var response = await client.SourceFiles.DownloadReviewedSourceFiles(intProjectId, intBuildId);
        
        var file = await FileDownloader.DownloadFileBytes(response.Url);
        file.Name = $"{buildId}.zip";
        
        return new(file);
    }
    
    [Action("Download reviewed source files", Description = "Download reviewed source files of specific build")]
    public async Task<DownloadFilesResponse> DownloadReviewedSourceFiles([ActionParameter] ProjectRequest project,
        [ActionParameter] [Display("Build ID")]
        string buildId)
    {
        var zip = await DownloadReviewedSourceFilesAsZip(project, buildId);
        var files = zip.File.Bytes.GetFilesFromZip();

        var result = new List<File>();
        await foreach (var file in files)
            // Removing folders from the final result
            if(file.Bytes.Any())
                result.Add(file);

        return new(result);
    }
}