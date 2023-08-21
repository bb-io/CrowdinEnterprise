using Blackbird.Applications.Sdk.Utils.Models;

namespace Apps.CrowdinEnterprise.Models.Response.File;

public record DownloadFilesResponse(List<FileEntity> Files);