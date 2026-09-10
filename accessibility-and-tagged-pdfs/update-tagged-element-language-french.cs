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
        const string outputPath = "output_french.pdf";
        const string targetElementId = "elem1"; // ID of the element to update

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (required for structure manipulation)
            ITaggedContent tagged = doc.TaggedContent;

            // Optionally set the document's default language to French
            tagged.SetLanguage("fr-FR");

            // Get the root of the structure tree
            StructureElement root = tagged.RootElement;

            // Locate the specific structure element by its ID (recursive search)
            StructureElement target = null;
            var allElements = root.FindElements<StructureElement>(true);
            foreach (var el in allElements)
            {
                if (el.ID == targetElementId)
                {
                    target = el;
                    break;
                }
            }

            if (target != null)
            {
                // Update the language property of the found element to French
                target.Language = "fr-FR";
                Console.WriteLine($"Updated language of element ID '{targetElementId}' to French.");
            }
            else
            {
                Console.WriteLine($"Element with ID '{targetElementId}' not found.");
            }

            // Save the modified PDF (PDF format is implicit)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}