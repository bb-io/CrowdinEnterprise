using Apps.CrowdinEnterprise.Models.Entities;

namespace Apps.CrowdinEnterprise.Models.Response.Task;

public record ListTasksResponse(TaskEntity[] Tasks);