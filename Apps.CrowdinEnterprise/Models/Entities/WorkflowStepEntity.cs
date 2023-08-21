using Crowdin.Api.Workflows;

namespace Apps.CrowdinEnterprise.Models.Entities;

public class WorkflowStepEntity
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string[] Languages { get; set; }

    public WorkflowStepEntity(WorkflowStep workflowStep)
    {
        Id = workflowStep.Id.ToString();
        Title = workflowStep.Title;
        Type = workflowStep.Type.ToString();
        Languages = workflowStep.Languages;
    }
}