﻿using Apps.CrowdinEnterprise.Api;
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
using Crowdin.Api;
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

    [Action("Export glossary", Description = "Export glossary as TBX file")]
    public async Task<ExportGlossaryResponse> ExportGlossaryAsync([ActionParameter] GetGlossaryRequest request)
    {
        var client = new CrowdinEnterpriseClient(Creds);

        var glossaryId = int.Parse(request.GlossaryId);
        
        var exportGlossary = await client.ExportGlossaryAsync(glossaryId);
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
    
    [Action("Import glossary", Description = "Import glossary from TBX file")]
    public async Task<ImportGlossaryResponse> ImportGlossaryAsync(
        [ActionParameter] Apps.CrowdinEnterprise.Models.Request.Glossary.ImportGlossaryRequest request)
    {
        var client = new CrowdinEnterpriseClient(Creds);
        using var memoryStream = new MemoryStream();

        await using var file = await _fileManagementClient.DownloadAsync(request.File);
            
        var fileMemoryStream = new MemoryStream();
        await file.CopyToAsync(fileMemoryStream);

        var glossaryImporter = new GlossaryImporter(fileMemoryStream);
        var xDocument = await glossaryImporter.ConvertToCrowdinFormat();

        xDocument.Save(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);

        string glossaryName = request.GlossaryName ?? request.File.Name.Replace(".tbx", string.Empty);
        string languageCode = request.LanguageCode ?? "en";
        var glossaryResponse = await client.Glossaries.AddGlossary(new AddGlossaryRequest
        {
            Name = glossaryName,
            LanguageId = languageCode,
            GroupId = string.IsNullOrEmpty(request.GroupId) ? null : int.Parse(request.GroupId)
        });

        var storageResponse =
            await client.Storage.AddStorage(memoryStream, request.File?.Name ?? $"{glossaryName}.tbx");
        var importGlossaryRequest = new Crowdin.Api.Glossaries.ImportGlossaryRequest
        {
            StorageId = storageResponse.Id,
            FirstLineContainsHeader = false
        };

        var response =
            await client.Glossaries.ImportGlossary(glossaryResponse.Id, importGlossaryRequest);
        if (response.Status != OperationStatus.Created && response.Status != OperationStatus.Finished &&
            response.Status != OperationStatus.InProgress)
        {
            throw new Exception($"Glossary import failed, status: {response.Status}");
        }

        return new ImportGlossaryResponse
        {
            Identifier = response.Identifier,
            Status = response.Status.ToString(),
            Progress = response.Progress.ToString()
        };
    }
}