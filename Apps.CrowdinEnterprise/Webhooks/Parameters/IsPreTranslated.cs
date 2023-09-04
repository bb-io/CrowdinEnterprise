using Blackbird.Applications.Sdk.Common;

namespace Apps.CrowdinEnterprise.Webhooks.Parameters
{
    public class IsPreTranslated
    {
        [Display("Is old string pre-translated?")]
        public bool? IsOldPretransalted { get; set;  }

        [Display("Is new string pre-translated?")]
        public bool? IsNewPretransalted { get; set; }
    }
}
