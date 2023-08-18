using System.IdentityModel.Tokens.Jwt;
using Apps.CrowdinEnterprise.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.CrowdinEnterprise.Connections;

public class ConnectionDefinition : IConnectionDefinition
{
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>
    {
        new()
        {
            Name = "OAuth",
            AuthenticationType = ConnectionAuthenticationType.OAuth2,
            ConnectionUsage = ConnectionUsage.Actions,
            ConnectionProperties = new List<ConnectionProperty>()
        }
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values)
    {
        var token = values.First(x => x.Key == CredsNames.ApiToken).Value;

        var creds = values
            .Select(x => new AuthenticationCredentialsProvider(
                AuthenticationCredentialsRequestLocation.None, x.Key, x.Value))
            .ToList();

        var domain = GetOrganization(token);
        creds.Add(new(AuthenticationCredentialsRequestLocation.None, CredsNames.OrganiationDomain, domain));

        return creds;
    }

    private string GetOrganization(string token)
    {
        var jwt = new JwtSecurityToken(token);
        return jwt.Claims.First(c => c.Type == CredsNames.OrganiationDomain).Value;
    }
}