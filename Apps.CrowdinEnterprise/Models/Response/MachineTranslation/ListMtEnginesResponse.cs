using Apps.CrowdinEnterprise.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.CrowdinEnterprise.Models.Response.MachineTranslation;

public class ListMtEnginesResponse
{
    [Display("Machine translation engines")]
    public MtEngineEntity[] MtEgines { get; set; }

    public ListMtEnginesResponse(MtEngineEntity[] items)
    {
        MtEgines = items;
    }
}