using Apps.CrowdinEnterprise.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.CrowdinEnterprise.Actions.Base;

public class CrowdinActions : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected CrowdinEnterpriseClient Client { get; }
    
    public CrowdinActions(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new(Creds);
    }
}