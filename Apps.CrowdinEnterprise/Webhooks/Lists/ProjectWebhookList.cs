﻿using Apps.CrowdinEnterprise.Webhooks.Handlers.Project.File;
using Apps.CrowdinEnterprise.Webhooks.Handlers.Project.Project;
using Apps.CrowdinEnterprise.Webhooks.Handlers.Project.String;
using Apps.CrowdinEnterprise.Webhooks.Handlers.Project.StringComment;
using Apps.CrowdinEnterprise.Webhooks.Handlers.Project.Suggestion;
using Apps.CrowdinEnterprise.Webhooks.Handlers.Project.Task;
using Apps.CrowdinEnterprise.Webhooks.Handlers.Project.Translation;
using Apps.CrowdinEnterprise.Webhooks.Models;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.File.Response;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.File.Wrappers;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.Project.Response;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.Project.Wrappers;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.String.Response;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.String.Wrappers;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.StringComment.Response;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.StringComment.Wrappers;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.Suggestion.Response;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.Suggestion.Wrappers;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.Task.Response;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.Task.Wrapper;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.Translation.Response;
using Apps.CrowdinEnterprise.Webhooks.Models.Payload.Translation.Wrappers;
using Apps.CrowdinEnterprise.Webhooks.Parameters;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using System.Net;
using Apps.CrowdinEnterprise.Models.Request.File;
using Apps.CrowdinEnterprise.Models.Request.Project;
using Apps.CrowdinEnterprise.Models.Request.SourceString;
using Apps.CrowdinEnterprise.Models.Request.Suggestions;
using Apps.CrowdinEnterprise.Models.Request.Task;
using Apps.CrowdinEnterprise.Webhooks.Models.Inputs;

namespace Apps.CrowdinEnterprise.Webhooks.Lists;

[WebhookList]
public class ProjectWebhookList
{
    #region File

    [Webhook("On file added", typeof(FileAddedHandler), Description = "On file added")]
    public Task<WebhookResponse<FileWithUserWebhookResponse>> OnFileAdded(WebhookRequest webhookRequest)
        => HandleWehookRequest<FileWithUserWrapper, FileWithUserWebhookResponse>(webhookRequest);

    [Webhook("On file approved", typeof(FileApprovedHandler), Description = "On file approved")]
    public Task<WebhookResponse<FileWithLanguageWebhookResponse>> OnFileApproved(WebhookRequest webhookRequest,
        [WebhookParameter] GetProjectOptionalRequest projectOptionalRequest,
        [WebhookParameter] GetFileOptionalRequest fileOptionalRequest)
    {
        var result = HandleWehookRequest<FileWithLanguageWrapper, FileWithLanguageWebhookResponse>(webhookRequest);

        if (projectOptionalRequest.ProjectId != null &&
            projectOptionalRequest.ProjectId != result.Result.Result?.File.ProjectId)
        {
            return Task.FromResult(PreflightResponse<FileWithLanguageWebhookResponse>());
        }
        
        if(fileOptionalRequest.FileId != null &&
           fileOptionalRequest.FileId != result.Result.Result?.File.Id)
        {
            return Task.FromResult(PreflightResponse<FileWithLanguageWebhookResponse>());
        }

        return result;
    }

    [Webhook("On file deleted", typeof(FileDeletedHandler), Description = "On file deleted")]
    public Task<WebhookResponse<FileWithUserWebhookResponse>> OnFileDeleted(WebhookRequest webhookRequest)
        => HandleWehookRequest<FileWithUserWrapper, FileWithUserWebhookResponse>(webhookRequest);

    [Webhook("On file reverted", typeof(FileRevertedHandler), Description = "On file reverted")]
    public Task<WebhookResponse<FileWithUserWebhookResponse>> OnFileReverted(WebhookRequest webhookRequest,
        [WebhookParameter] GetProjectOptionalRequest projectOptionalRequest,
        [WebhookParameter] GetFileOptionalRequest fileOptionalRequest)
    {
        var result = HandleWehookRequest<FileWithUserWrapper, FileWithUserWebhookResponse>(webhookRequest);
        
        if (projectOptionalRequest.ProjectId != null &&
            projectOptionalRequest.ProjectId != result.Result.Result?.File.ProjectId)
        {
            return Task.FromResult(PreflightResponse<FileWithUserWebhookResponse>());
        }
        
        if(fileOptionalRequest.FileId != null &&
           fileOptionalRequest.FileId != result.Result.Result?.File.Id)
        {
            return Task.FromResult(PreflightResponse<FileWithUserWebhookResponse>());
        }
        
        return result;
    }

    [Webhook("On file translated", typeof(FileTranslatedHandler), Description = "On file fully translated")]
    public Task<WebhookResponse<FileWithLanguageWebhookResponse>> OnFileTranslated(WebhookRequest webhookRequest,
        [WebhookParameter] GetProjectOptionalRequest projectOptionalRequest,
        [WebhookParameter] GetFileOptionalRequest fileOptionalRequest)
    {
        var result = HandleWehookRequest<FileWithLanguageWrapper, FileWithLanguageWebhookResponse>(webhookRequest);
        
        if (projectOptionalRequest.ProjectId != null &&
            projectOptionalRequest.ProjectId != result.Result.Result?.File.ProjectId)
        {
            return Task.FromResult(PreflightResponse<FileWithLanguageWebhookResponse>());
        }
        
        if(fileOptionalRequest.FileId != null &&
           fileOptionalRequest.FileId != result.Result.Result?.File.Id)
        {
            return Task.FromResult(PreflightResponse<FileWithLanguageWebhookResponse>());
        }
        
        return result;
    }
    
    [Webhook("On file updated", typeof(FileUpdatedHandler), Description = "On file updated")]
    public Task<WebhookResponse<FileWithUserWebhookResponse>> OnFileUpdated(WebhookRequest webhookRequest,
        [WebhookParameter] GetProjectOptionalRequest projectOptionalRequest,
        [WebhookParameter] GetFileOptionalRequest fileOptionalRequest)
    {
        var result = HandleWehookRequest<FileWithUserWrapper, FileWithUserWebhookResponse>(webhookRequest);
        
        if (projectOptionalRequest.ProjectId != null &&
            projectOptionalRequest.ProjectId != result.Result.Result?.File.ProjectId)
        {
            return Task.FromResult(PreflightResponse<FileWithUserWebhookResponse>());
        }
        
        if(fileOptionalRequest.FileId != null &&
           fileOptionalRequest.FileId != result.Result.Result?.File.Id)
        {
            return Task.FromResult(PreflightResponse<FileWithUserWebhookResponse>());
        }
        
        return result;
    }

    #endregion

    #region Project

    [Webhook("On project approved", typeof(ProjectApprovedHandler), Description = "On project approved")]
    public Task<WebhookResponse<ProjectWithLanguageWebhookResponse>> OnProjectApproved(WebhookRequest webhookRequest,
        [WebhookParameter] GetProjectOptionalRequest projectOptionalRequest)
    {
        var result = HandleWehookRequest<ProjectWithLanguageWrapper, ProjectWithLanguageWebhookResponse>(webhookRequest);
        
        if (projectOptionalRequest.ProjectId != null &&
            projectOptionalRequest.ProjectId != result.Result.Result?.Project.Id)
        {
            return Task.FromResult(PreflightResponse<ProjectWithLanguageWebhookResponse>());
        }
        
        return result;
    }

    [Webhook("On project translated", typeof(ProjectTranslatedHandler), Description = "On project translated")]
    public Task<WebhookResponse<ProjectWithLanguageWebhookResponse>> OnProjectTranslated(WebhookRequest webhookRequest,
        [WebhookParameter] GetProjectOptionalRequest projectOptionalRequest)
    {
        var result = HandleWehookRequest<ProjectWithLanguageWrapper, ProjectWithLanguageWebhookResponse>(webhookRequest);
        
        if (projectOptionalRequest.ProjectId != null &&
            projectOptionalRequest.ProjectId != result.Result.Result?.Project.Id)
        {
            return Task.FromResult(PreflightResponse<ProjectWithLanguageWebhookResponse>());
        }
        
        return result;
    }
    
    [Webhook("On project built", typeof(ProjectBuiltHandler), Description = "On project built")]
    public Task<WebhookResponse<ProjectBuiltWebhookResponse>> OnProjectBuilt(WebhookRequest webhookRequest)
        => HandleWehookRequest<ProjectBuildWrapper, ProjectBuiltWebhookResponse>(webhookRequest);

    #endregion

    #region String

    [Webhook("On string added", typeof(StringAddedHandler), Description = "On string added")]
    public Task<WebhookResponse<SourceStringWebhookResponse>> OnStringAdded(WebhookRequest webhookRequest)
        => HandleWehookRequest<EventsWebhookResponse<SourceStringWrapper>, SourceStringWebhookResponse>(webhookRequest);

    [Webhook("On string deleted", typeof(StringDeletedHandler), Description = "On string deleted")]
    public Task<WebhookResponse<SourceStringWebhookResponse>> OnStringDeleted(WebhookRequest webhookRequest)
        => HandleWehookRequest<EventsWebhookResponse<SourceStringWrapper>, SourceStringWebhookResponse>(webhookRequest);

    [Webhook("On string updated", typeof(StringUpdatedHandler), Description = "On string updated")]
    public Task<WebhookResponse<SourceStringWebhookResponse>> OnStringUpdated(WebhookRequest webhookRequest,
        [WebhookParameter] GetProjectOptionalRequest projectOptionalRequest,
        [WebhookParameter] GetFileOptionalRequest fileOptionalRequest,
        [WebhookParameter] GetSourceStringOptionalRequest stringOptionalRequest)
    {
        var result =HandleWehookRequest<EventsWebhookResponse<SourceStringWrapper>, SourceStringWebhookResponse>(webhookRequest);
        
        if (projectOptionalRequest.ProjectId != null &&
            projectOptionalRequest.ProjectId != result.Result.Result?.String.ProjectId)
        {
            return Task.FromResult(PreflightResponse<SourceStringWebhookResponse>());
        }
        
        if(fileOptionalRequest.FileId != null &&
           fileOptionalRequest.FileId != result.Result.Result?.String.FileId)
        {
            return Task.FromResult(PreflightResponse<SourceStringWebhookResponse>());
        }
        
        if(stringOptionalRequest.StringId != null &&
           stringOptionalRequest.StringId != result.Result.Result?.String.Id)
        {
            return Task.FromResult(PreflightResponse<SourceStringWebhookResponse>());
        }
        
        return result;
    }

    #endregion

    #region StringComment

    [Webhook("On string comment created", typeof(StringCommentCreatedHandler),
         Description = "On string comment created")]
    public async Task<WebhookResponse<StringCommentWebhookResponse>> OnStringCommentCreated(WebhookRequest webhookRequest, [WebhookParameter] ContainsInputRequest input)
    {

        var response = await HandleWehookRequest<StringCommentWrapper, StringCommentWebhookResponse>(webhookRequest);

        if (!string.IsNullOrWhiteSpace(input.Text))
        {
            //if it`s null or result does not contain the input string, it will stop the webhook
            if (response.Result == null || !response.Result.Text.Contains(input.Text))
            {
                return PreflightResponse<StringCommentWebhookResponse>();
            }
        }

        return response;
    }

    [Webhook("On string comment deleted", typeof(StringCommentDeletedHandler), Description = "On string comment deleted")]
    public Task<WebhookResponse<StringCommentWebhookResponse>> OnStringCommentDeleted(WebhookRequest webhookRequest)
        => HandleWehookRequest<StringCommentWrapper, StringCommentWebhookResponse>(webhookRequest);

    [Webhook("On string comment restored", typeof(StringCommentRestoredHandler), Description = "On string comment restored")]
    public Task<WebhookResponse<StringCommentWebhookResponse>> OnStringCommentRestored(WebhookRequest webhookRequest)
        => HandleWehookRequest<StringCommentWrapper, StringCommentWebhookResponse>(webhookRequest);

    [Webhook("On string comment updated", typeof(StringCommentUpdatedHandler), Description = "On string comment updated")]
    public Task<WebhookResponse<StringCommentWebhookResponse>> OnStringCommentUpdated(WebhookRequest webhookRequest)
        => HandleWehookRequest<StringCommentWrapper, StringCommentWebhookResponse>(webhookRequest);

    #endregion

    #region Suggestion

    [Webhook("On suggestion added", typeof(SuggestionAddedHandler), Description = "On suggestion added")]
    public Task<WebhookResponse<SuggestionWebhookResponse>> OnSuggestionAdded(WebhookRequest webhookRequest,
        [WebhookParameter] GetProjectOptionalRequest projectOptionalRequest,
        [WebhookParameter] GetSuggestionOptionalRequest fileOptionalRequest)
    {
        var result = HandleWehookRequest<SuggestionWrapper, SuggestionWebhookResponse>(webhookRequest);
        
        if (projectOptionalRequest.ProjectId != null &&
            projectOptionalRequest.ProjectId != result.Result.Result?.ProjectId)
        {
            return Task.FromResult(PreflightResponse<SuggestionWebhookResponse>());
        }
        
        if(fileOptionalRequest.SuggestionId != null &&
           fileOptionalRequest.SuggestionId != result.Result.Result?.Id)
        {
            return Task.FromResult(PreflightResponse<SuggestionWebhookResponse>());
        }
        
        return result;
    }

    [Webhook("On suggestion approved", typeof(SuggestionApprovedHandler), Description = "On suggestion approved")]
    public Task<WebhookResponse<SuggestionWebhookResponse>> OnSuggestionApproved(WebhookRequest webhookRequest,
        [WebhookParameter] GetProjectOptionalRequest projectOptionalRequest,
        [WebhookParameter] GetSuggestionOptionalRequest fileOptionalRequest)
    {
        var result = HandleWehookRequest<SuggestionWrapper, SuggestionWebhookResponse>(webhookRequest);
        
        if (projectOptionalRequest.ProjectId != null &&
            projectOptionalRequest.ProjectId != result.Result.Result?.ProjectId)
        {
            return Task.FromResult(PreflightResponse<SuggestionWebhookResponse>());
        }
        
        if(fileOptionalRequest.SuggestionId != null &&
           fileOptionalRequest.SuggestionId != result.Result.Result?.Id)
        {
            return Task.FromResult(PreflightResponse<SuggestionWebhookResponse>());
        }
        
        return result;
    }

    [Webhook("On suggestion deleted", typeof(SuggestionDeletedHandler), Description = "On suggestion deleted")]
    public Task<WebhookResponse<SuggestionWebhookResponse>> OnSuggestionDeleted(WebhookRequest webhookRequest)
        => HandleWehookRequest<SuggestionWrapper, SuggestionWebhookResponse>(webhookRequest);

    [Webhook("On suggestion disapproved", typeof(SuggestionDisapprovedHandler), Description = "On suggestion disapproved")]
    public Task<WebhookResponse<SuggestionWebhookResponse>> OnSuggestionDisapproved(WebhookRequest webhookRequest,
        [WebhookParameter] GetProjectOptionalRequest projectOptionalRequest,
        [WebhookParameter] GetSuggestionOptionalRequest fileOptionalRequest)
    {
        var result = HandleWehookRequest<SuggestionWrapper, SuggestionWebhookResponse>(webhookRequest);
        
        if (projectOptionalRequest.ProjectId != null &&
            projectOptionalRequest.ProjectId != result.Result.Result?.ProjectId)
        {
            return Task.FromResult(PreflightResponse<SuggestionWebhookResponse>());
        }
        
        if(fileOptionalRequest.SuggestionId != null &&
           fileOptionalRequest.SuggestionId != result.Result.Result?.Id)
        {
            return Task.FromResult(PreflightResponse<SuggestionWebhookResponse>());
        }
        
        return result;
    }

    [Webhook("On suggestion updated", typeof(SuggestionUpdatedHandler), Description = "On suggestion updated")]
    public Task<WebhookResponse<SuggestionWebhookResponse>> OnSuggestionUpdated(WebhookRequest webhookRequest)
        => HandleWehookRequest<SuggestionWrapper, SuggestionWebhookResponse>(webhookRequest);

    #endregion

    #region Task

    [Webhook("On task added", typeof(TaskAddedHandler), Description = "On task added")]
    public Task<WebhookResponse<TaskWebhookResponse>> OnTaskAdded(WebhookRequest webhookRequest)
        => HandleWehookRequest<TaskWrapper, TaskWebhookResponse>(webhookRequest);

    [Webhook("On task deleted", typeof(TaskDeletedHandler), Description = "On task deleted")]
    public Task<WebhookResponse<TaskWebhookResponse>> OnTaskDeleted(WebhookRequest webhookRequest)
        => HandleWehookRequest<TaskWrapper, TaskWebhookResponse>(webhookRequest);

    [Webhook("On task status changed", typeof(TaskStatusChangedHandler), Description = "On task status changed")]
    public Task<WebhookResponse<TaskStatusChangedWebhookResponse>> OnTaskStatusChanged(WebhookRequest webhookRequest,
        [WebhookParameter] GetProjectOptionalRequest projectOptionalRequest,
        [WebhookParameter] GetTaskOptionalRequest taskOptionalRequest)
    {
        var result = HandleWehookRequest<TaskStatusChangedWrapper, TaskStatusChangedWebhookResponse>(webhookRequest);
        
        if (projectOptionalRequest.ProjectId != null &&
            projectOptionalRequest.ProjectId != result.Result.Result?.ProjectId)
        {
            return Task.FromResult(PreflightResponse<TaskStatusChangedWebhookResponse>());
        }
        
        if(taskOptionalRequest.TaskId != null &&
           taskOptionalRequest.TaskId != result.Result.Result?.Id)
        {
            return Task.FromResult(PreflightResponse<TaskStatusChangedWebhookResponse>());
        }
        
        return result;
    }

    #endregion

    #region Translation

    [Webhook("On translation updated", typeof(TranslationUpdatedHandler), Description = "On translation updated")]
    public WebhookResponse<TranslationUpdatedWebhookResponse> OnTranslationUpdated(WebhookRequest webhookRequest, 
        [WebhookParameter] IsPreTranslated pretranslated,
        [WebhookParameter] GetProjectOptionalRequest projectOptionalRequest,
        [WebhookParameter] GetFileOptionalRequest fileOptionalRequest,
        [WebhookParameter] GetSourceStringOptionalRequest sourceStringOptionalRequest)
    {
        var data = JsonConvert.DeserializeObject<TranslationUpdatedWrapper>(webhookRequest.Body.ToString());

        if (data is null)
            throw new InvalidCastException(nameof(webhookRequest.Body));

        if (pretranslated.IsNewPretransalted != null && pretranslated.IsNewPretransalted.Value != data.NewTranslation.IsPretranslated)
            return new WebhookResponse<TranslationUpdatedWebhookResponse> { HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK), ReceivedWebhookRequestType = WebhookRequestType.Preflight };

        if (pretranslated.IsOldPretransalted != null && pretranslated.IsOldPretransalted.Value != data.OldTranslation.IsPretranslated)
            return new WebhookResponse<TranslationUpdatedWebhookResponse> { HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK), ReceivedWebhookRequestType = WebhookRequestType.Preflight };

        var result = new TranslationUpdatedWebhookResponse();
        result.ConfigureResponse(data);

        if (projectOptionalRequest.ProjectId != null &&
            projectOptionalRequest.ProjectId != result.NewTranslation.SourceString.ProjectId)
        {
            return PreflightResponse<TranslationUpdatedWebhookResponse>();
        }
        
        if(fileOptionalRequest.FileId != null &&
           fileOptionalRequest.FileId != result.NewTranslation.SourceString.FileId)
        {
            return PreflightResponse<TranslationUpdatedWebhookResponse>();
        }
        
        if(sourceStringOptionalRequest.StringId != null &&
           sourceStringOptionalRequest.StringId != result.NewTranslation.SourceString.Id)
        {
            return PreflightResponse<TranslationUpdatedWebhookResponse>();
        }

        return new WebhookResponse<TranslationUpdatedWebhookResponse>
        {
            Result = result
        };
    }

    #endregion

    public async Task<WebhookResponse<TV>> HandleWehookRequest<T, TV>(WebhookRequest webhookRequest)
        where TV : CrowdinWebhookResponse<T>, new()
    {
        var data = JsonConvert.DeserializeObject<T>(webhookRequest.Body.ToString());

        if (data is null)
            throw new InvalidCastException(nameof(webhookRequest.Body));

        var result = new TV();
        result.ConfigureResponse(data);

        return new WebhookResponse<TV>
        {
            Result = result
        };
    }
    
    private static WebhookResponse<T> PreflightResponse<T>()
        where T : class
    {
        return new WebhookResponse<T>
        {
            ReceivedWebhookRequestType = WebhookRequestType.Preflight,
            Result = null
        };
    }
}