using System.Text.Json;
using Apps.CrowdinEnterprise.Constants;
using Apps.CrowdinEnterprise.Models.Response.Glossary;
using Apps.CrowdinEnterprise.Models.Responses;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Crowdin.Api;
using Crowdin.Api.Glossaries;
using RestSharp;

namespace Apps.CrowdinEnterprise.Api;

public class CrowdinEnterpriseClient : CrowdinApiClient
{
    private readonly AuthenticationCredentialsProvider[] creds;
    
    public CrowdinEnterpriseClient(AuthenticationCredentialsProvider[] creds) 
        : base(GetCrowdinCreds(creds))
    {
        this.creds = creds;
    }

    private static CrowdinCredentials GetCrowdinCreds(
        AuthenticationCredentialsProvider[] creds)
    {
        var token = creds.Get(CredsNames.AccessToken);
        var organization = creds.Get(CredsNames.OrganiationDomain);
    
        return new()
        {
            AccessToken = token.Value,
            Organization = organization.Value
        };
    }
    
    public async Task<ExportGlossaryModel> ExportGlossaryAsync(int glossaryId)
    {
        var organization = creds.Get(CredsNames.OrganiationDomain).Value;
        var token = creds.Get(CredsNames.AccessToken).Value;
        
        var restClient = new RestClient($"https://{organization}.api.crowdin.com/api/v2");
        
        var restRequest = new RestRequest($"/glossaries/{glossaryId}/exports", Method.Post)
            .WithHeaders(new Dictionary<string, string> { {"Authorization", $"Bearer {token}"}})
            .WithJsonBody(new { format = "tbx" });
    
        var exportGlossary = await restClient.ExecuteAsync<GlossaryExportStatus>(restRequest);

        var data = JsonSerializer.Deserialize<GlossaryExportResponse>(exportGlossary.Content);
        return data.Data;
    }
}