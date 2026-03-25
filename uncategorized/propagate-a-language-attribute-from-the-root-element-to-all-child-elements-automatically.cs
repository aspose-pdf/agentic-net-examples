using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string language = "en-US";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Set document‑level language (writes /Lang entry)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage(language);

            // Set language on the root structure element
            StructureElement root = tagged.RootElement;
            root.Language = language;

            // Propagate to every descendant element
            PropagateLanguage(root, language);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Language propagated and saved to '{outputPath}'.");
    }

    static void PropagateLanguage(StructureElement element, string lang)
    {
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                se.Language = lang;
                PropagateLanguage(se, lang);
            }
        }
    }
}