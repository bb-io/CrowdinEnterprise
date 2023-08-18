﻿using Apps.CrowdinEnterprise.Api;
using Apps.CrowdinEnterprise.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.CrowdinEnterprise.DataSourceHandlers;

public class StorageDataHandler : BaseInvocable, IAsyncDataSourceHandler
{
    private AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public StorageDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var client = new CrowdinEnterpriseClient(Creds);

        var items = await Paginator.Paginate((lim, offset)
            => client.Storage.ListStorages(lim, offset));
        
        return items
            .Where(x => context.SearchString == null ||
                        x.FileName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x.Id.ToString(), x => x.FileName);
    }
}