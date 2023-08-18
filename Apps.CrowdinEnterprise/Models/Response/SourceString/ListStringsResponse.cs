using Apps.CrowdinEnterprise.Models.Entities;

namespace Apps.CrowdinEnterprise.Models.Response.SourceString;

public record ListStringsResponse(SourceStringEntity[] Strings);