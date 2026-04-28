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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set the language on the document level (root element)
            tagged.SetLanguage(language);

            // Get the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // Propagate the language to all descendant elements recursively
            PropagateLanguage(root, language);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Language '{language}' propagated and saved to '{outputPath}'.");
    }

    // Recursively set Language on each StructureElement in the tree
    static void PropagateLanguage(StructureElement element, string lang)
    {
        // Set language on the current element
        element.Language = lang;

        // Iterate over child elements
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                PropagateLanguage(se, lang);
            }
        }
    }
}