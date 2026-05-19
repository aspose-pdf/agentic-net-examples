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

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Set the document language (applies where not overridden)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage(defaultLang);

            // Get the root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Recursively assign language to elements that lack it
            SetMissingLanguage(root, defaultLang);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }

    // Recursively walks the structure tree and sets Language where it is null or empty
    static void SetMissingLanguage(StructureElement element, string lang)
    {
        if (string.IsNullOrEmpty(element.Language))
        {
            element.Language = lang;
        }

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                SetMissingLanguage(se, lang);
            }
        }
    }
}