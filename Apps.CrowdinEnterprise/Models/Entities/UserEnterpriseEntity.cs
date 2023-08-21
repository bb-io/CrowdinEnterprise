using Blackbird.Applications.Sdk.Common;
using Crowdin.Api.Users;

namespace Apps.CrowdinEnterprise.Models.Entities;

public class UserEnterpriseEntity
{
    [Display("ID")] public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    [Display("First name")] public string FirstName { get; set; }
    [Display("Last name")] public string LastName { get; set; }

    public string Status { get; set; }
    [Display("Avatar url")] public string AvatarUrl { get; set; }
    [Display("Created at")] public DateTime CreatedAt { get; set; }
    [Display("Last seen")] public DateTime? LastSeen { get; set; }
    [Display("Is two factor enabled")] public bool IsTwoFactorEnabled { get; set; }
    public string Timezone { get; set; }
    
    [Display("Is admin")]
    public bool IsAdmin { get; set; }

    public UserEnterpriseEntity(UserEnterprise user)
    {
        Id = user.Id.ToString();
        Username = user.Username;
        Email = user.Email;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Status = user.Status.ToString();
        AvatarUrl = user.AvatarUrl;
        CreatedAt = user.CreatedAt.DateTime;
        LastSeen = user.LastSeen?.DateTime;
        IsTwoFactorEnabled = user.TwoFactor is UserTwoFactorStatus.Enabled;
        IsAdmin = user.IsAdmin;
        Timezone = user.TimeZone;
    }
}