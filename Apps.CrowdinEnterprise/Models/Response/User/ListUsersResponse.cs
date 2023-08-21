using Apps.CrowdinEnterprise.Models.Entities;

namespace Apps.CrowdinEnterprise.Models.Response.User;

public record ListUsersResponse(UserEnterpriseEntity[] Users);
