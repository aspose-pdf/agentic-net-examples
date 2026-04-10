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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set a default language for the whole document (if not already set)
            tagged.SetLanguage("en-US");

            // Get the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // Retrieve all structure elements recursively
            var allElements = root.FindElements<StructureElement>(true);

            // Assign a language to any element that lacks one
            foreach (StructureElement elem in allElements)
            {
                if (string.IsNullOrEmpty(elem.Language))
                {
                    elem.Language = "en-US";
                }
            }

            // Ensure the root element itself also has a language
            if (string.IsNullOrEmpty(root.Language))
            {
                root.Language = "en-US";
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}