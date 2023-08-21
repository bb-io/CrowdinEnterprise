using Apps.CrowdinEnterprise.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Models.Request.Team;

public class TeamRequest
{
    [Display("Team")]
    [DataSource(typeof(TeamDataHandler))]
    public string TeamId { get; set; }
}