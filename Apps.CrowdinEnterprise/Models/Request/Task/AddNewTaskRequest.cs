using Apps.CrowdinEnterprise.DataSourceHandlers;
using Apps.CrowdinEnterprise.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Models.Request.Task;

public class AddNewTaskRequest
{
    [Display("Workflow step ID")]
    public string WorkflowStepId { get; set; }
    public string Title { get; set; }

    [Display("Language")]
    [DataSource(typeof(LanguagesDataHandler))]
    public string LanguageId { get; set; }

    [Display("File IDs")]
    public IEnumerable<string> FileIds { get; set; }

    [StaticDataSource(typeof(TaskStatusHandler))]
    public string? Status { get; set; }

    public string? Description { get; set; }

    [Display("Split files")]
    public bool? SplitFiles { get; set; }

    [Display("Skip assigned strings")]
    public bool? SkipAssignedStrings { get; set; }

    [Display("Label IDs")]
    public IEnumerable<string>? LabelIds { get; set; }

    public DateTime? Deadline { get; set; }

    [Display("Date from")]
    public DateTime? DateFrom { get; set; }

    [Display("Date to")]
    public DateTime? DateTo { get; set; }
}