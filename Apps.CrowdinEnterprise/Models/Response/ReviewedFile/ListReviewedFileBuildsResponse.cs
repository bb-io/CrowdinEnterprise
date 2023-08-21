using Apps.CrowdinEnterprise.Models.Entities;

namespace Apps.CrowdinEnterprise.Models.Response.ReviewedFile;

public record ListReviewedFileBuildsResponse(ReviewedFileBuildEntity[] Builds);