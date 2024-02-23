﻿using Apps.CrowdinEnterprise.Connections.OAuth;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.CrowdinEnterprise;

//TODO:
// user management
// team actions
    
public class CrowdinEnterpriseApplication : BaseInvocable, IApplication
{
    public string Name
    {
        get => "Crowdin Enterprise";
        set { }
    }

    public IPublicApplicationMetadata? PublicApplicationMetadata { get; }

    private readonly Dictionary<Type, object> _typesInstances;

    public CrowdinEnterpriseApplication(InvocationContext invocationContext) : base(invocationContext)
    {
        _typesInstances = CreateTypesInstances();
    }

    public T GetInstance<T>()
    {
        if (!_typesInstances.TryGetValue(typeof(T), out var value))
        {
            throw new InvalidOperationException($"Instance of type '{typeof(T)}' not found");
        }

        return (T)value;
    }

    private Dictionary<Type, object> CreateTypesInstances()
    {
        return new Dictionary<Type, object>
        {
            { typeof(IOAuth2AuthorizeService), new OAuth2AuthorizationSerivce(InvocationContext) },
            { typeof(IOAuth2TokenService), new OAuth2TokenService(InvocationContext) }
        };
    }
}