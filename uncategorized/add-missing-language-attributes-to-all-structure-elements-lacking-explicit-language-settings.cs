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
        const string defaultLang = "en-US";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Set document‑level language if not already set
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage(defaultLang);

            // Traverse the logical structure and assign language where missing
            StructureElement root = tagged.RootElement;
            SetLanguageRecursively(root, defaultLang);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Language attributes added and saved to '{outputPath}'.");
    }

    static void SetLanguageRecursively(StructureElement element, string lang)
    {
        if (string.IsNullOrEmpty(element.Language))
        {
            element.Language = lang;
        }

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                SetLanguageRecursively(se, lang);
            }
        }
    }
}