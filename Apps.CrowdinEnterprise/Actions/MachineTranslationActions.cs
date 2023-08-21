using Apps.CrowdinEnterprise.Api;
using Apps.CrowdinEnterprise.Constants;
using Apps.CrowdinEnterprise.DataSourceHandlers;
using Apps.CrowdinEnterprise.Models.Entities;
using Apps.CrowdinEnterprise.Models.Request.MachineTranslation;
using Apps.CrowdinEnterprise.Models.Response.MachineTranslation;
using Apps.CrowdinEnterprise.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Parsers;
using Crowdin.Api.MachineTranslationEngines;

namespace Apps.CrowdinEnterprise.Actions;

[ActionList]
public class MachineTranslationActions : BaseInvocable
{
    private AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    public MachineTranslationActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("List machine translation engines", Description = "List all machine translation engines")]
    public async Task<ListMtEnginesResponse> ListMtEnginges(
        [ActionParameter] [Display("Group ID")] [DataSource(typeof(ProjectGroupDataHandler))]
        string? groupId)
    {
        var intGroupId = IntParser.Parse(groupId, nameof(groupId));

        var client = new CrowdinEnterpriseClient(Creds);

        var items = await Paginator.Paginate((lim, offset)
            => client.MachineTranslationEngines.ListMts(intGroupId, lim, offset));

        var mtEntities = items.Select(x => new MtEngineEntity(x)).ToArray();
        return new(mtEntities);
    }

    [Action("Translate via machine translation engine", Description = "Translate text via machine translation engine")]
    public async Task<MtTextTranslationEntity> TranslateTextViaMt(
        [ActionParameter] MtEngineRequest mtEngine,
        [ActionParameter] TranslateTextRequest input)
    {
        var response = await TranslateLinesViaMt(mtEngine, new(input));
        return new(response);
    }

    [Action("Translate lines via machine translation engine",
        Description = "Translate multiple text lines via machine translation engine")]
    public async Task<MtStringsTranslationEntity> TranslateLinesViaMt(
        [ActionParameter] MtEngineRequest mtEngine,
        [ActionParameter] TranslateStringsRequest input)
    {
        var intMtId = IntParser.Parse(mtEngine.MtEngineId, nameof(mtEngine.MtEngineId));

        var recognitionProvider =
            EnumParser.Parse<LanguageRecognitionProvider>(input.LanguageRecognitionProvider,
                nameof(input.LanguageRecognitionProvider),
                EnumValues.LanguageRecognitionProvider);

        var client = new CrowdinEnterpriseClient(Creds);

        var request = new TranslateViaMtRequest
        {
            TargetLanguageId = input.TargetLanguageId,
            SourceLanguageId = input.SourceLanguageId,
            Strings = input.Text.ToList(),
            LanguageRecognitionProvider = recognitionProvider
        };

        var response = await client.MachineTranslationEngines
            .TranslateViaMt(intMtId!.Value, request);
        return new(response);
    }
}