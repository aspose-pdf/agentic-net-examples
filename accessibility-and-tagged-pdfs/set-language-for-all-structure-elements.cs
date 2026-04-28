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
        const string language   = "en-US";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set the document‑level language (applies where not overridden)
            tagged.SetLanguage(language);

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Retrieve all structure elements in the tree (recursive)
            var allElements = root.FindElements<StructureElement>(true);

            // Assign language to any element that does not already have one
            foreach (StructureElement element in allElements)
            {
                if (string.IsNullOrEmpty(element.Language))
                {
                    element.Language = language;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}