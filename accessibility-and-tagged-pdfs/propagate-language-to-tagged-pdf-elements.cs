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
        const string language   = "en-US";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Set the document language (metadata)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage(language);

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Set language on the root element
            root.Language = language;

            // Recursively propagate the language to all descendants
            PropagateLanguage(root, language);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Language '{language}' propagated and saved to '{outputPath}'.");
    }

    // Recursively sets the Language property on the element and all its children
    static void PropagateLanguage(StructureElement element, string language)
    {
        // Set language on the current element
        element.Language = language;

        // Iterate over child elements
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStructure)
            {
                PropagateLanguage(childStructure, language);
            }
        }
    }
}