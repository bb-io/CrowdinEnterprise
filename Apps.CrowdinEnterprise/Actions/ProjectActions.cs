using Apps.CrowdinEnterprise.Api;
using Apps.CrowdinEnterprise.Constants;
using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Models.Request.Project;
using Apps.CrowdinEnterprise.Models.Response.Project;
using Apps.CrowdinEnterprise.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Parsers;
using Crowdin.Api.ProjectsGroups;

namespace Apps.CrowdinEnterprise.Actions;

[ActionList]
public class ProjectActions : BaseInvocable
{
    private AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public ProjectActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }
    
    [Action("List projects", Description = "List all projects")]
    public async Task<ListProjectsResponse> ListProjects([ActionParameter] ListProjectsRequest input)
    {
        var userId = IntParser.Parse(input.UserId, nameof(input.UserId));
        var groupId = IntParser.Parse(input.GroupID, nameof(input.GroupID));
        
        var client = new CrowdinEnterpriseClient(Creds);

        var items = await Paginator.Paginate((lim, offset)
            => client.ProjectsGroups.ListProjects<EnterpriseProject>(userId, groupId, input.HasManagerAccess ?? false, lim, offset));

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
        
        var request = new EnterpriseProjectForm()
        {
            Name = input.Name,
            SourceLanguageId = input.SourceLanguageId,
            Identifier = input.Identifier,
            Visibility = EnumParser.Parse<ProjectVisibility>(input.Visibility, nameof(input.Visibility), EnumValues.ProjectVisibility),
            TargetLanguageIds = input.TargetLanguageIds?.ToList(),
            Cname = input.Cname,
            Description = input.Description,
            IsMtAllowed = input.IsMtAllowed,
            AutoSubstitution = input.AutoSubstitution,
            AutoTranslateDialects = input.AutoTranslateDialects,
            PublicDownloads = input.PublicDownloads,
            HiddenStringsProofreadersAccess = input.HiddenStringsProofreadersAccess,
            UseGlobalTm = input.UseGlobalTm,
            SkipUntranslatedStrings = input.SkipUntranslatedStrings,
            SkipUntranslatedFiles = input.SkipUntranslatedFiles,
            ExportApprovedOnly = input.ExportApprovedOnly,
            InContext = input.InContext,
            InContextProcessHiddenStrings = input.InContextProcessHiddenStrings,
            InContextPseudoLanguageId = input.InContextPseudoLanguageId,
            QaCheckIsActive = input.QaCheckIsActive,
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
}