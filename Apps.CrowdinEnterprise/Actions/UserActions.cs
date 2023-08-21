using Apps.CrowdinEnterprise.Api;
using Apps.CrowdinEnterprise.Constants;
using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Models.Request.User;
using Apps.CrowdinEnterprise.Models.Response.User;
using Apps.CrowdinEnterprise.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Parsers;
using Crowdin.Api.Users;

namespace Apps.CrowdinEnterprise.Actions;

[ActionList]
public class UserActions : BaseInvocable
{
    private AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public UserActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("List users", Description = "List all users")]
    public async Task<ListUsersResponse> ListUsers([ActionParameter] ListUsersRequest input)
    {
        var client = new CrowdinEnterpriseClient(Creds);

        var userStatus = EnumParser.Parse<UserStatus>(input.Status, nameof(input.Status), EnumValues.UserStatus);
        var twoFactor = EnumParser.Parse<UserTwoFactorStatus>(input.TwoFactor, nameof(input.TwoFactor),
            EnumValues.UserTwoFactorStatus);

        var response = await Paginator.Paginate((lim, offset) =>
            client.Users.ListUsers(userStatus, input.Search, twoFactor, lim, offset));

        var users = response.Select(x => new UserEnterpriseEntity(x)).ToArray();
        return new(users);
    }

    [Action("Get user", Description = "Get specific user")]
    public async Task<UserEnterpriseEntity> GetUser(
        [ActionParameter] UserRequest user)
    {
        var intUserId = IntParser.Parse(user.UserId, nameof(user.UserId))!.Value;
        var client = new CrowdinEnterpriseClient(Creds);

        var response = await client.Users.GetUser(intUserId);
        return new(response);
    }

    [Action("Invite user", Description = "Add a new user")]
    public async Task<UserEnterpriseEntity> InviteUser(
        [ActionParameter] InviteUserRequest input)
    {
        var client = new CrowdinEnterpriseClient(Creds);

        var response = await client.Users.InviteUser(new()
        {
            Email = input.Email,
            FirstName = input.FirstName,
            LastName = input.LastName,
            TimeZone = input.Timezone
        });
        
        return new(response);
    }

    [Action("Delete user", Description = "Delete specific user")]
    public Task DeleteUser(
        [ActionParameter] UserRequest user)
    {
        var client = new CrowdinEnterpriseClient(Creds);
        return client.Users.DeleteUser(IntParser.Parse(user.UserId, nameof(user.UserId))!.Value);
    }
}