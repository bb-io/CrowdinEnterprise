using Apps.CrowdinEnterprise.Api;
using Apps.CrowdinEnterprise.DataSourceHandlers;
using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Models.Request.Group;
using Apps.CrowdinEnterprise.Models.Response.Group;
using Apps.CrowdinEnterprise.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Parsers;

namespace Apps.CrowdinEnterprise.Actions;

[ActionList]
public class ProjectGroupActions : BaseInvocable
{
    private AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public ProjectGroupActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("List project groups", Description = "List all project groups")]
    public async Task<ListGroupsResponse> ListProjectGroups(
        [ActionParameter] [Display("Parent group ID")] [DataSource(typeof(ProjectGroupDataHandler))]
        string? parentId)
    {
        var intParentId = IntParser.Parse(parentId, nameof(parentId));
        var client = new CrowdinEnterpriseClient(Creds);

        var response = await Paginator.Paginate((lim, offset) =>
            client.ProjectsGroups.ListGroups(intParentId, lim, offset));

        var groups = response.Select(x => new GroupEntity(x)).ToArray();
        return new(groups);
    }

    [Action("Get project group", Description = "Get specific project group")]
    public async Task<GroupEntity> GetProjectGroup(
        [ActionParameter] ProjectGroupRequest group)
    {
        var intGroupId = IntParser.Parse(group.ProjectGroupId, nameof(group.ProjectGroupId))!.Value;
        var client = new CrowdinEnterpriseClient(Creds);

        var response = await client.ProjectsGroups.GetGroup(intGroupId);
        return new(response);
    }

    [Action("Add project group", Description = "Add a new project group")]
    public async Task<GroupEntity> AddProjectGroup(
        [ActionParameter] AddProjectGroupRequest input)
    {
        var client = new CrowdinEnterpriseClient(Creds);

        var response = await client.ProjectsGroups.AddGroup(new()
        {
            Name = input.Name,
            Description = input.Description,
            ParentId = IntParser.Parse(input.ParentId, nameof(input.ParentId))
        });
        return new(response);
    }

    [Action("Delete project group", Description = "Delete specific project group")]
    public Task DeleteProjectGroup(
        [ActionParameter] ProjectGroupRequest group)
    {
        var client = new CrowdinEnterpriseClient(Creds);
        return client.ProjectsGroups.DeleteGroup(IntParser.Parse(group.ProjectGroupId, nameof(group.ProjectGroupId))!.Value);
    }
}