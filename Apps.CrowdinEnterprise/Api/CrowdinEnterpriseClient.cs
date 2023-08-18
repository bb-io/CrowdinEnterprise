using Apps.CrowdinEnterprise.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Crowdin.Api;

namespace Apps.CrowdinEnterprise.Api;

public class CrowdinEnterpriseClient : CrowdinApiClient
{
    public CrowdinEnterpriseClient(AuthenticationCredentialsProvider[] creds) 
        : base(GetCrowdinCreds(creds))
    {
    }

    private static CrowdinCredentials GetCrowdinCreds(
        AuthenticationCredentialsProvider[] creds)
    {
        var token = creds.Get(CredsNames.ApiToken);
        var organization = creds.Get(CredsNames.OrganiationDomain);
    
        return new()
        {
            AccessToken = token.Value,
            Organization = organization.Value
        };
    }
}