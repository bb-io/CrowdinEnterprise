using Apps.CrowdinEnterprise.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.CrowdinEnterprise.Models.Response.TranslationMemory;

public record ListTranslationMemoriesResponse([property: Display("Translation memories")]
    TranslationMemoryEntity[] TranslationMemories);