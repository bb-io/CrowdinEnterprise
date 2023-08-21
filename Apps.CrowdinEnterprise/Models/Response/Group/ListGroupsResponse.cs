using Apps.CrowdinEnterprise.Models.Entities;

namespace Apps.CrowdinEnterprise.Models.Response.Group;

public record ListGroupsResponse(GroupEntity[] Groups);