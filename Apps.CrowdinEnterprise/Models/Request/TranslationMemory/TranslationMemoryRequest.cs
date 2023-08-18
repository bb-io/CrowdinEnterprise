﻿using Apps.CrowdinEnterprise.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Models.Request.TranslationMemory;

public class TranslationMemoryRequest
{
    [Display("Translation memory")]
    [DataSource(typeof(TmDataHandler))]
    public string TranslationMemoryId { get; set; }
}