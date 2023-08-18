using Apps.CrowdinEnterprise.DataSourceHandlers;
using Apps.CrowdinEnterprise.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.CrowdinEnterprise.Models.Request.MachineTranslation;

public class TranslateStringsRequest
{
    [Display("Source language")] 
    [DataSource(typeof(LanguagesDataHandler))]
    public string SourceLanguageId { get; set; }
    
    [Display("Target language")] 
    [DataSource(typeof(LanguagesDataHandler))]
    public string TargetLanguageId { get; set; }
    
    public IEnumerable<string> Text { get; set; }
    
    [Display("Language recognition provider")] 
    [DataSource(typeof(LanguageRecognitionProviderHandler))]
    public string LanguageRecognitionProvider { get; set; }

    
    public TranslateStringsRequest()
    {
    }

    public TranslateStringsRequest(TranslateTextRequest input)
    {
        TargetLanguageId = input.TargetLanguageId;
        SourceLanguageId = input.SourceLanguageId;
        LanguageRecognitionProvider = input.LanguageRecognitionProvider;
        Text = new[] { input.Text };
    }
}