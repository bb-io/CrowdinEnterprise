using Apps.CrowdinEnterprise.Connections.OAuth;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;

namespace Apps.CrowdinEnterprise;

//TODO:
// add project groups
// reviewed source files
// add/delete mte
// user management
// team actions
// group created/deleted webhooks
// check if webhooks return enterprise project models
    
public class CrowdinEnterpriseApplication : IApplication
{enterprise
    public string Name
    {
        get => "Crowdin Enterprise";
        set { }
    }

    private readonly Dictionary<Type, object> _typesInstances;

    public CrowdinEnterpriseApplication()
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
            { typeof(IOAuth2AuthorizeService), new OAuth2AuthorizationSerivce() },
            { typeof(IOAuth2TokenService), new OAuth2TokenService() }
        };
    }
}