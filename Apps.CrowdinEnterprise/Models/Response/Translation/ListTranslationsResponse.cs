using Apps.CrowdinEnterprise.Models.Entities;

namespace Apps.CrowdinEnterprise.Models.Response.Translation;

public record ListTranslationsResponse(TranslationEntity[] Translations);