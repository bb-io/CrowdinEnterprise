using System.Xml.Linq;
using Blackbird.Applications.Sdk.Glossaries.Utils.Dtos;
using Blackbird.Applications.Sdk.Glossaries.Utils.Dtos.Enums;

namespace Apps.CrowdinEnterprise.Utils;

public class GlossaryExporter(Stream inputFileStream)
{
    public async Task<Glossary> ExportGlossaryAsync(string glossaryTitle)
    {
        XDocument crowdinTbx = XDocument.Load(inputFileStream);

        var sourceDesc = ExtractSourceDescription(crowdinTbx);
        var glossaryConceptEntries = ParseGlossaryConceptEntries(crowdinTbx);

        var glossary = new Glossary(glossaryConceptEntries)
        {
            Title = glossaryTitle,
            SourceDescription = sourceDesc,
        };

        return glossary;

        /*
        await using var tbxStream = glossary.ConvertToTbx();
        var tbxFileReference = await fileManagementClient.UploadAsync(tbxStream, MediaTypeNames.Text.Xml,
            $"{tbxFileName}.txb");
        return tbxFileReference;
        */
    }

    private string ExtractSourceDescription(XDocument document)
    {
        return document.Descendants("sourceDesc").FirstOrDefault()?.Value ?? "Default Description";
    }

    private List<GlossaryConceptEntry> ParseGlossaryConceptEntries(XDocument document)
    {
        var glossaryConceptEntries = new List<GlossaryConceptEntry>();

        foreach (var termEntry in document.Descendants("termEntry"))
        {
            var conceptId = termEntry.Attribute("id")?.Value ?? Guid.NewGuid().ToString();
            var langSets = termEntry.Elements("langSet");
            
            string definition = string.Empty;
            foreach (var langSet in langSets)
            {
                definition = langSet.Descendants("descrip").FirstOrDefault(x => x.Attributes().First(x => x.Name == "type").Value != string.Empty).Value;
            }
            
            var languageSections = langSets.Select(langSet => ParseLanguageSection(langSet)).Where(section => section != null).ToList();

            if (languageSections.Any())
            {
                var entry = new GlossaryConceptEntry(conceptId, languageSections) { Definition = definition };
                glossaryConceptEntries.Add(entry);
            }
        }

        return glossaryConceptEntries;
    }

    private GlossaryLanguageSection ParseLanguageSection(XElement langSet)
    {
        var lang = langSet.Attribute(XNamespace.Xml + "lang")?.Value;
        if (string.IsNullOrWhiteSpace(lang)) return null;

        var terms = langSet.Descendants("term").Select(term => new GlossaryTermSection(term.Value)
        {
            PartOfSpeech = ParseFromTbxPartOfSpeech(langSet.Descendants("termNote")
                .FirstOrDefault(x => x.Attribute("type")?.Value == "partOfSpeech")?.Value)
        }).ToList();

        return terms.Count > 0 ? new GlossaryLanguageSection(lang, terms) : null;
    }

    private PartOfSpeech ParseFromTbxPartOfSpeech(string partOfSpeech)
    {
        return partOfSpeech switch
        {
            "adjective" => PartOfSpeech.Adjective,
            "adverb" => PartOfSpeech.Adverb,
            "conjunction" => PartOfSpeech.Conjunction,
            "interjection" => PartOfSpeech.Interjection,
            "noun" => PartOfSpeech.Noun,
            "preposition" => PartOfSpeech.Preposition,
            "pronoun" => PartOfSpeech.Pronoun,
            "proper noun" => PartOfSpeech.ProperNoun,
            "verb" => PartOfSpeech.Verb,
            _ => PartOfSpeech.Noun 
        };
    }
}