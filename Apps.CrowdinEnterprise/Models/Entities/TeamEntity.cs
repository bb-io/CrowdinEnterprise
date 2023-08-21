using Blackbird.Applications.Sdk.Common;
using Crowdin.Api.Teams;

namespace Apps.CrowdinEnterprise.Models.Entities;

public class TeamEntity
{
    [Display("ID")]
    public string Id { get; set; }

    public string Name { get; set; }

    [Display("Total members")]
    public int TotalMembers { get; set; }
    
    [Display("Created at")]
    public DateTimeOffset CreatedAt { get; set; }

    [Display("Updated at")]
    public DateTimeOffset? UpdatedAt { get; set; }
    
    public TeamEntity(Team team)
    {
        Id = team.Id.ToString();
        Name = team.Name;
        TotalMembers = team.TotalMembers;
        CreatedAt = team.CreatedAt;
        UpdatedAt = team.CreatedAt;
    }
}