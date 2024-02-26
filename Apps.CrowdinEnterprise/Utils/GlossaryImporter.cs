﻿using System.Xml.Linq;
using Blackbird.Applications.Sdk.Glossaries.Utils.Converters;

namespace Apps.CrowdinEnterprise.Utils;

public class GlossaryImporter(Stream fileStream)
{
    public async Task<string> ConvertToCrowdinFormat()
    {
        try
        {
            return string.Empty;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Exeption type: {e.GetType()}, Message: {e.Message}");
        }
    }
    
    private async Task<XDocument> ConvertToCrowdinFormatAsync()
    {
        if (fileStream.Length == 0)
        {
            throw new InvalidOperationException("The file stream is empty.");
        }
        
        fileStream.Position = 0;
        var glossary = await fileStream.ConvertFromTbx();

        string sourceDesc = glossary.SourceDescription;

        // var outputDoc = new XDocument(
        //     new XDeclaration("1.0", "utf-8", null),
        //     new XElement("martif",
        //         new XAttribute("type", "TBX-Basic"),
        //         new XAttribute(XNamespace.Xml + "lang", "en"),
        //         new XElement("martifHeader",
        //             new XElement("fileDesc",
        //                 new XElement("sourceDesc", new XElement("p", sourceDesc))
        //             ),
        //             new XElement("encodingDesc",
        //                 new XElement("p", new XAttribute("type", "XCSURI"),
        //                     "http://www.lisa.org/fileadmin/standards/tbx_basic/TBXBasicXCSV02.xcs")
        //             )
        //         ),
        //         new XElement("text",
        //             new XElement("body", glossary.ConceptEntries.Select(conceptEntry => new XElement("termEntry",
        //                     new XAttribute("id", conceptEntry.Id),
        //                     conceptEntry.LanguageSections.Select(langSec => new XElement("langSet",
        //                         new XAttribute(XNamespace.Xml + "lang", langSec.LanguageCode),
        //                         new XElement("tig",
        //                             new XElement("term",
        //                                 new XAttribute("id", $"{conceptEntry.Id}-{langSec.LanguageCode}"),
        //                                 langSec.Terms.First().Term),
        //                             new XElement("termNote",
        //                                 new XAttribute("type", "partOfSpeech"),
        //                                 langSec.Terms.First().PartOfSpeech.ToString()?.ToLower()),
        //                             new XElement("descrip",
        //                                 new XAttribute("type", "definition"),
        //                                 conceptEntry.Definition)
        //                         )))
        //                 )).ToList()
        //             )
        //         )
        //     )
        // );
        
        return null;
    }
}