using Apps.CrowdinEnterprise.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.CrowdinEnterprise.Models.Request.File.Base;

public class ManageFileRequest
{
    [Display("Storage")]
    [DataSource(typeof(StorageDataHandler))]
    public string? StorageId { get; set; }

    public FileReference? File { get; set; }
}