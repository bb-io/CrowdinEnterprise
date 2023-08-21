using Blackbird.Applications.Sdk.Common;
using Crowdin.Api.ProjectsGroups;

namespace Apps.CrowdinEnterprise.Models.Entities;

public class GroupEntity
{
    [Display("ID")]
    public string Id { get; set; }

    [Display("Name")]
    public string Name { get; set; }

    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [Display("Updated at")]
    public DateTime? UpdatedAt { get; set; }

    [Display("Description")]
    public string Description { get; set; }

    [Display("Parent ID")]
    public string ParentId { get; set; }

    [Display("User ID")]
    public string UserId { get; set; }

    [Display("Organization ID")]
    public string OrganizationId { get; set; }

    public GroupEntity(Group group)
    {
        Id = group.Id.ToString();
        Name = group.Name;
        CreatedAt = group.CreatedAt.DateTime;
        UpdatedAt = group.UpdatedAt?.DateTime;
        Description = group.Description;
        ParentId = group.ParentId.ToString();
        UserId = group.UserId.ToString();
        OrganizationId = group.OrganizationId.ToString();
    }
}