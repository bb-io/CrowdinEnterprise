﻿using Apps.CrowdinEnterprise.Webhooks.Handlers.Base;
using Apps.CrowdinEnterprise.Webhooks.Models.Inputs;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Crowdin.Api.Webhooks;

namespace Apps.CrowdinEnterprise.Webhooks.Handlers.Project.StringComment;

public class StringCommentRestoredHandler : ProjectWebhookHandler
{
    protected override EventType SubscriptionEvent => EventType.StringCommentRestored;

    public StringCommentRestoredHandler([WebhookParameter(true)] ProjectWebhookInput input) : base(input)
    {
    }
}