using Apps.CrowdinEnterprise.Api;
using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Models.Request.Project;
using Apps.CrowdinEnterprise.Models.Response.Workflow;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Parsers;
using Crowdin.Api.Workflows;

namespace Apps.CrowdinEnterprise.Actions;

[ActionList]
public class WorkflowActions : BaseInvocable
{
    private AuthenticationCredentialsProvider[] Creds 
        => InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public WorkflowActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }
        
    [Action("List workflow steps", Description = "List all workflow steps of project")]
    public async Task<ListWorkflowStepsResponse> ListWorkflowSteps([ActionParameter] ProjectRequest project)
    {
        var intProjectId = IntParser.Parse(project.ProjectId, nameof(project.ProjectId))!.Value;

        var client = new CrowdinEnterpriseClient(Creds);
        var executor = new WorkflowsApiExecutor(client);

        var response = await executor.ListWorkflowSteps(intProjectId);
        var workflowSteps = response.Data.Select(x => new WorkflowStepEntity(x)).ToArray();
        
        return new(workflowSteps);
    }
}