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
        const string outputPath = "output_tagged.pdf";
        const string defaultLang = "en-US";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content
                ITaggedContent tagged = doc.TaggedContent;

                // Set document language if not already set
                tagged.SetLanguage(defaultLang);

                // Get the root structure element (no cast needed)
                StructureElement root = tagged.RootElement;

                // Recursively set language on all elements that lack it
                SetMissingLanguage(root, defaultLang);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Recursively walk the structure tree and assign Language where missing
    static void SetMissingLanguage(StructureElement element, string language)
    {
        if (string.IsNullOrEmpty(element.Language))
        {
            element.Language = language;
        }

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                SetMissingLanguage(se, language);
            }
        }
    }
}