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
        const string outputPath = "output_with_language.pdf";
        const string language   = "en-US";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language on the root element
            StructureElement root = tagged.RootElement;
            root.Language = language;

            // Propagate language to all descendants
            PropagateLanguage(root, language);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with language '{language}' to '{outputPath}'.");
    }

    // Recursively set the Language property on each structure element
    static void PropagateLanguage(StructureElement element, string lang)
    {
        element.Language = lang;
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                PropagateLanguage(se, lang);
        }
    }
}