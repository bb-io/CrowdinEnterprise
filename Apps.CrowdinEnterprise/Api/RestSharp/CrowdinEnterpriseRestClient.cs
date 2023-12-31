﻿using Apps.CrowdinEnterprise.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.Extensions.String;
using RestSharp;

namespace Apps.CrowdinEnterprise.Api.RestSharp;

public class CrowdinEnterpriseRestClient : RestClient
{
    public CrowdinEnterpriseRestClient(IEnumerable<AuthenticationCredentialsProvider> creds) : base(GetRestClientOptions(creds))
    {
        
    }

    private static RestClientOptions GetRestClientOptions(IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var domain = creds.Get(CredsNames.OrganiationDomain).Value;
        
        return new()
        {
            BaseUrl = $"https://{domain}.api.crowdin.com/api/v2".ToUri()
        };
    }
}