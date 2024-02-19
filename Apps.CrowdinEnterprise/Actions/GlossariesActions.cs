using Apps.CrowdinEnterprise.Api;
using Apps.CrowdinEnterprise.Models.Request.Glossary;
using Apps.CrowdinEnterprise.Models.Response.Glossary;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Crowdin.Api.Glossaries;

namespace Apps.CrowdinEnterprise.Actions;

[ActionList]
public class GlossariesActions : BaseInvocable
{
    private AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();
    
    private readonly IFileManagementClient _fileManagementClient;

    public GlossariesActions(InvocationContext invocationContext, IFileManagementClient client) : base(invocationContext)
    {
        _fileManagementClient = client;
    }
    
    [Action("Export glossary", Description = "Export glossary from Crowdin Enterprise project")]
    public async Task<ExportGlossaryResponse> ExportGlossaryAsync([ActionParameter] GetGlossaryRequest request)
    {
        var client = new CrowdinEnterpriseClient(Creds);
        
        var glossaryId = int.Parse(request.GlossaryId);
        
        var exportGlossary = await client.Glossaries.ExportGlossary(glossaryId, new ExportGlossaryRequest { Format = GlossaryFormat.Tbx });
        var downloadLink = await client.Glossaries.DownloadGlossary(glossaryId, exportGlossary.Identifier);
        
        var stream = await _fileManagementClient.DownloadAsync(new FileReference(new HttpRequestMessage(HttpMethod.Get, downloadLink.Url)));
        
        var glossary = await client.Glossaries.GetGlossary(glossaryId);
        var file = await _fileManagementClient.UploadAsync(stream, "text/tbx", $"{glossary.Name}.tbx");
        
        return new(file);
    }
}