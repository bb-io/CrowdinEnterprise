namespace Apps.CrowdinEnterprise.Constants;

public static class EnumValues
{
    public static readonly string[] TaskStatus = { "Todo", "InProgress", "", "Closed" };
    public static readonly string[] TmFileFormat = { "Tmx", "Csv", "Xlsx" };
    public static readonly string[] LanguageRecognitionProvider = { "Crowdin", "Engine" };
    public static readonly string[] TaskType = { "Translate", "Proofread", "TranslateByVendor", "ProofreadByVendor" };
    public static readonly string[] StringCommentType = { "Comment", "Issue" };
    public static readonly string[] IssueStatus = { "Resolved", "Unresolved" };

    public static readonly string[] IssueType =
        { "GeneralQuestion", "TranslationMistake", "ContextRequest", "SourceMistake" };

    public static readonly string[] PluralCategoryName = { "Zero", "One", "Two", "Few", "Many", "Other" };
    public static readonly string[] StringScope = { "Identifier", "Text", "Context" };
    public static readonly string[] ProjectVisibility = { "Open", "Private" };

    public static readonly string[] TranslateDuplicates =
    {
        "Show",
        "Hide",
        "ShowAndAutoTranslate",
        "ShowWithVersionBranch",
        "HideStrict",
        "ShowWithVersionBranchStrict"
    };

    public static readonly string[] TagsDetection =
    {
        "Auto",
        "Count",
        "Skip"
    };

    public static readonly string[] UserStatus =
    {
        "Active",
        "Pending",
        "Blocked"
    };

    public static readonly string[] UserTwoFactorStatus =
    {
        "Enabled",
        "Disabled"
    };
}