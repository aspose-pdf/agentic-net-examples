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

        // Load the PDF and work with its tagged content
        using (Document doc = new Document(inputPath))
        {
            // Set the document language if not already set
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage(defaultLang);

            // Get the root of the structure tree
            StructureElement root = tagged.RootElement;

            // Recursively ensure every element has a language attribute
            SetMissingLanguage(root, defaultLang);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }

    // Recursively set Language on elements that lack it
    static void SetMissingLanguage(StructureElement element, string language)
    {
        // If Language is null or empty, assign the default language
        if (string.IsNullOrEmpty(element.Language))
        {
            element.Language = language;
        }

        // Iterate over child elements
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                SetMissingLanguage(se, language);
            }
        }
    }
}