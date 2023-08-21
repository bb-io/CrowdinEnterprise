using Apps.CrowdinEnterprise.Api;
using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Models.Request.Team;
using Apps.CrowdinEnterprise.Models.Response.Team;
using Apps.CrowdinEnterprise.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Parsers;

namespace Apps.CrowdinEnterprise.Actions;

[ActionList]
public class TeamActions : BaseInvocable
{
    private AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public TeamActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("List teams", Description = "List all teams")]
    public async Task<ListTeamsResponse> ListTeams()
    {
        var client = new CrowdinEnterpriseClient(Creds);

        var response = await Paginator.Paginate(client.Teams.ListTeams);

        var teams = response.Select(x => new TeamEntity(x)).ToArray();
        return new(teams);
    }

    [Action("Get team", Description = "Get specific team")]
    public async Task<TeamEntity> GetTeam(
        [ActionParameter] TeamRequest team)
    {
        var intTeamId = IntParser.Parse(team.TeamId, nameof(team.TeamId))!.Value;
        var client = new CrowdinEnterpriseClient(Creds);

        var response = await client.Teams.GetTeam(intTeamId);
        return new(response);
    }

    [Action("Add team", Description = "Add a new team")]
    public async Task<TeamEntity> AddTeam(
        [ActionParameter] [Display("Name")] string name)
    {
        var client = new CrowdinEnterpriseClient(Creds);

        var response = await client.Teams.AddTeam(new()
        {
            Name = name
        });
        
        return new(response);
    }

    [Action("Delete team", Description = "Delete specific team")]
    public Task DeleteTeam(
        [ActionParameter] TeamRequest team)
    {
        var client = new CrowdinEnterpriseClient(Creds);
        return client.Teams.DeleteTeam(IntParser.Parse(team.TeamId, nameof(team.TeamId))!.Value);
    }
}