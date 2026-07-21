using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string defaultLang = "en-US";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Set the document language (write‑only) if needed
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage(defaultLang);

            // Recursively assign language to every structure element that lacks it
            StructureElement root = tagged.RootElement;
            SetLanguageRecursive(root, defaultLang);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with language attributes to '{outputPath}'.");
    }

    // Walk the structure tree and set Language where it is null or empty
    static void SetLanguageRecursive(StructureElement element, string lang)
    {
        if (string.IsNullOrEmpty(element.Language))
        {
            element.Language = lang;
        }

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                SetLanguageRecursive(se, lang);
            }
        }
    }
}