using Apps.CrowdinEnterprise.Api;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.CrowdinEnterprise.Connections;

public class ConnectionValidator : IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authProviders, CancellationToken cancellationToken)
    {
        var client = new CrowdinEnterpriseClient(authProviders.ToArray());

        try
        {
            await client.Languages.ListSupportedLanguages();

            return new()
            {
                IsValid = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }
    }
}