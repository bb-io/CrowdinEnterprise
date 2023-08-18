using Apps.CrowdinEnterprise.Api;
using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Models.Request.Storage;
using Apps.CrowdinEnterprise.Models.Response.Storage;
using Apps.CrowdinEnterprise.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Parsers;

namespace Apps.CrowdinEnterprise.Actions;

[ActionList]
public class StorageActions : BaseInvocable
{
    private AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public StorageActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }
    
    [Action("List storages", Description = "List all storages")]
    public async Task<ListStoragesResponse> ListStorages()
    {
        var client = new CrowdinEnterpriseClient(Creds);

        var responseItems = await Paginator.Paginate((lim, offset)
            => client.Storage.ListStorages(lim, offset));

        var storages = responseItems.Select(x => new StorageEntity(x)).ToArray();
        return new(storages);
    }

    [Action("Get storage", Description = "Get specific storage")]
    public async Task<StorageEntity> GetStorage([ActionParameter] StorageRequest storage)
    {
        var intStorageId = IntParser.Parse(storage.StorageId, nameof(storage.StorageId));
        var client = new CrowdinEnterpriseClient(Creds);

        var response = await client.Storage.GetStorage(intStorageId!.Value);
        return new(response);
    }
    
    [Action("Add storage", Description = "Add new storage")]
    public async Task<StorageEntity> AddStorage([ActionParameter] AddStorageRequest input)
    {
        var client = new CrowdinEnterpriseClient(Creds);

        var response = await client.Storage
            .AddStorage(new MemoryStream(input.File), input.FileName);
        
        return new(response);
    }
    
    [Action("Delete storage", Description = "Delete specific storage")]
    public Task DeleteStorage([ActionParameter] StorageRequest storage)
    {
        var intStorageId = IntParser.Parse(storage.StorageId, nameof(storage.StorageId));
        var client = new CrowdinEnterpriseClient(Creds);

        return client.Storage.DeleteStorage(intStorageId!.Value);
    }
}