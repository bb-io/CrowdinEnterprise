﻿using Apps.CrowdinEnterprise.Api;
using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Models.Request.SourceString;
using Apps.CrowdinEnterprise.Models.Response.SourceString;
using Apps.CrowdinEnterprise.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Parsers;
using Crowdin.Api.SourceStrings;

namespace Apps.CrowdinEnterprise.Actions;

[ActionList]
public class SourceStringAction : BaseInvocable
{
    private AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public SourceStringAction(InvocationContext invocationContext) : base(invocationContext)
    {
    }
    
    [Action("List strings", Description = "List all project source strings")]
    public async Task<ListStringsResponse> ListStrings([ActionParameter] ListStringsRequest input)
    {
        var intProjectId = IntParser.Parse(input.ProjectId, nameof(input.ProjectId));

        var client = new CrowdinEnterpriseClient(Creds);

        var items = await Paginator.Paginate((lim, offset)
            =>
        {
            var request = new StringsListParams
            {
                Limit = lim,
                Offset = offset,
                FileId = IntParser.Parse(input.FileId, nameof(input.FileId)),
                BranchId = IntParser.Parse(input.BranchId, nameof(input.BranchId)),
                DirectoryId = IntParser.Parse(input.DirectoryId, nameof(input.DirectoryId)),
                LabelIds = input.LabelIds,
                Filter = input.Filter,
                CroQL = input.CroQl,
                Scope = EnumParser.Parse<StringScope>(input.Scope, nameof(input.Scope)),
                DenormalizePlaceholders = input.DenormalizePlaceholders is true ? 1 : 0,
            };
            return client.SourceStrings.ListStrings(intProjectId!.Value, request);
        });

        var strings = items.Select(x => new SourceStringEntity(x)).ToArray();
        return new(strings);
    }

    [Action("Get source string", Description = "Get specific source string")]
    public async Task<SourceStringEntity> GetString([ActionParameter] GetSourceStringRequest input)
    {
        var intProjectId = IntParser.Parse(input.ProjectId, nameof(input.ProjectId));
        var intStringId = IntParser.Parse(input.StringId, nameof(input.StringId));

        var client = new CrowdinEnterpriseClient(Creds);

        var response = await client.SourceStrings
            .GetString(intProjectId!.Value, intStringId!.Value, input.DenormalizePlaceholders ?? false);
        return new(response);
    }

    [Action("Add source string", Description = "Add new source string")]
    public async Task<SourceStringEntity> AddString([ActionParameter] AddSourceStringRequest input)
    {
        var intProjectId = IntParser.Parse(input.ProjectId, nameof(input.ProjectId));

        var client = new CrowdinEnterpriseClient(Creds);

        var request = new AddStringRequest
        {
            Text = input.Text,
            Identifier = input.Identifier,
            FileId = IntParser.Parse(input.FileId, nameof(input.FileId)),
            Context = input.Context,
            IsHidden = input.IsHidden,
            MaxLength = input.MaxLength,
            LabelIds = input.LabelIds?.Select(labelId => IntParser.Parse(labelId, nameof(labelId))!.Value).ToList()
        };
        var response = await client.SourceStrings
            .AddString(intProjectId!.Value, request);
        
        return new(response);
    }

    [Action("Delete source string", Description = "Delete specific source string")]
    public Task DeleteString([ActionParameter] DeleteSourceStringRequest input)
    {
        var intProjectId = IntParser.Parse(input.ProjectId, nameof(input.ProjectId));
        var intStringId = IntParser.Parse(input.StringId, nameof(input.StringId));

        var client = new CrowdinEnterpriseClient(Creds);

        return client.SourceStrings.DeleteString(intProjectId!.Value, intStringId!.Value);
    }
}