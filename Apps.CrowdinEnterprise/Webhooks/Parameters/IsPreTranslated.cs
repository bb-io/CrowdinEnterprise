using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
