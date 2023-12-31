﻿using Blackbird.Applications.Sdk.Common;
using Crowdin.Api.StringTranslations;

namespace Apps.CrowdinEnterprise.Models.Entities;

public class TranslationEntity
{
    [Display("ID")]
    public string Id { get; set; }
    
    [Display("User ID")]
    public string UserId { get; set; }
    public string Text { get; set; }
    public int? Rating { get; set; }
    
    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    public TranslationEntity(StringTranslation stringTranslation)
    {
        Id = stringTranslation.Id.ToString();
        Text = stringTranslation.Text;
        UserId = stringTranslation.User.Id.ToString();
        Rating = stringTranslation.Rating;
        CreatedAt = stringTranslation.CreatedAt.DateTime;
    }
    
    public TranslationEntity(PlainLanguageTranslations translation)
    {
        Id = translation.TranslationId.ToString();
        Text = translation.Text;
        UserId = translation.User.Id.ToString();
        CreatedAt = translation.CreatedAt.DateTime;
    }
}