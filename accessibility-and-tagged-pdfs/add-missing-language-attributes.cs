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
            ITaggedContent tagged = doc.TaggedContent;
            if (tagged == null)
            {
                Console.WriteLine("Document does not contain tagged content.");
                doc.Save(outputPath);
                return;
            }

            StructureElement root = tagged.RootElement;
            SetLanguageRecursive(root, defaultLang);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }

    static void SetLanguageRecursive(StructureElement element, string lang)
    {
        if (string.IsNullOrEmpty(element.Language))
            element.Language = lang;

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                SetLanguageRecursive(se, lang);
        }
    }
}