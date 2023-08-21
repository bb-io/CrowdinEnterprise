using Apps.CrowdinEnterprise.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Models.Request.User;

public class UserRequest
{
    [Display("User")]
    [DataSource(typeof(UserDataHandler))]
    public string UserId { get; set; }
}