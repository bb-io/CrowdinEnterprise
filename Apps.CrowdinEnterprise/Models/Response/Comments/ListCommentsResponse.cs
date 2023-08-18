using Apps.CrowdinEnterprise.Models.Entities;

namespace Apps.CrowdinEnterprise.Models.Response.Comments;

public record ListCommentsResponse(CommentEntity[] Comments);