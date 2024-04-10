using Apps.CrowdinEnterprise.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.CrowdinEnterprise.Models.Request.User;

public class ListUsersRequest
{
    [StaticDataSource(typeof(UserStatusDataHandler))]
    public string? Status { get; set; }
    public string? Search { get; set; }
    public string? TwoFactor { get; set; }
}