using Apps.CrowdinEnterprise.Models.Entities;

namespace Apps.CrowdinEnterprise.Models.Response.File;

public record ListFilesResponse(FileEntity[] Files);