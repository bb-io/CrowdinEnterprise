﻿using Apps.CrowdinEnterprise.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Models.Request.Storage;

public class StorageRequest
{
    [Display("Storage")]
    [DataSource(typeof(StorageDataHandler))]
    public string StorageId { get; set; }
}