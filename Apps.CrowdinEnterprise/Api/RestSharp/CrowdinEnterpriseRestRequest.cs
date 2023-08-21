using Apps.CrowdinEnterprise.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.CrowdinEnterprise.Api.RestSharp;

public class CrowdinEnterpriseRestRequest : RestRequest
{
    public CrowdinEnterpriseRestRequest(
        string source,
        Method method,
        IEnumerable<AuthenticationCredentialsProvider> creds) : base(source, method)
    {
        var token = creds.First(x => x.KeyName == CredsNames.AccessToken).Value;
        this.AddHeader("Authorization", $"Bearer {token}");
    }
}