using System.Net.Mime;
using Apps.CrowdinEnterprise.Api;
using Apps.CrowdinEnterprise.Models.Request.Glossary;
using Apps.CrowdinEnterprise.Models.Response.Glossary;
using Apps.CrowdinEnterprise.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Glossaries.Utils.Converters;
using Blackbird.Applications.Sdk.Utils.Utilities;
using Crowdin.Api.Glossaries;

namespace Apps.CrowdinEnterprise.Actions;

[ActionList]
public class GlossariesActions : BaseInvocable
{
    private AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    private readonly IFileManagementClient _fileManagementClient;

    public GlossariesActions(InvocationContext invocationContext, IFileManagementClient client) : base(
        invocationContext)
    {
        _fileManagementClient = client;
    }

    [Action("Export glossary", Description = "Export glossary from Crowdin Enterprise project")]
    public async Task<ExportGlossaryResponse> ExportGlossaryAsync([ActionParameter] GetGlossaryRequest request)
    {
        var client = new CrowdinEnterpriseClient(Creds);

        var glossaryId = int.Parse(request.GlossaryId);
        var exportGlossary =
            await client.Glossaries.ExportGlossary(glossaryId,
                new ExportGlossaryRequest { Format = GlossaryFormat.Tbx });
        var downloadLink = await client.Glossaries.DownloadGlossary(glossaryId, exportGlossary.Identifier);

        var fileContent = await FileDownloader.DownloadFileBytes(downloadLink.Url);

        string glossaryTitle = fileContent.Name ?? "Glossary.tbx";
        var glossaryExporter = new GlossaryExporter(fileContent.FileStream);
        var glossary = await glossaryExporter.ExportGlossaryAsync(glossaryTitle);

        await using var tbxStream = glossary.ConvertToTbx();
        string contentType = MimeTypes.GetMimeType(glossaryTitle);
        var tbxFileReference = await _fileManagementClient.UploadAsync(tbxStream, contentType,
            glossaryTitle);

        return new(tbxFileReference);
    }
}