using Apps.CrowdinEnterprise.Models.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.CrowdinEnterprise.Models.Response.Workflow;

public record ListWorkflowStepsResponse([property: Display("Workflow steps")] 
    WorkflowStepEntity[] WorkflowSteps);