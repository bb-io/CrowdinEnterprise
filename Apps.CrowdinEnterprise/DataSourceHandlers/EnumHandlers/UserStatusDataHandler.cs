using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.CrowdinEnterprise.DataSourceHandlers.EnumHandlers;

public class UserStatusDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "Active", "Active" },
        { "Pending", "Pending" },
        { "Blocked", "Blocked" },
    };
}