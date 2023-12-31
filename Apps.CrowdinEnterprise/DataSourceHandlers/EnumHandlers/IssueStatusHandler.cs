﻿using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.CrowdinEnterprise.DataSourceHandlers.EnumHandlers;

public class IssueStatusHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "Resolved", "Resolved" },
        { "Unresolved", "Unresolved" },
    };
}