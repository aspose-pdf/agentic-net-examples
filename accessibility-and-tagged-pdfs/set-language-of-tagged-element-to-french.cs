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
        const string outputPath = "output_french.pdf";
        const string targetElementId = "elem1"; // ID of the structure element to update

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and work with its tagged content
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Ensure the document has a structure tree
            StructureElement root = taggedContent.RootElement;
            if (root == null)
            {
                Console.Error.WriteLine("Document does not contain tagged structure.");
                return;
            }

            // Find the specific structure element by its ID (search recursively)
            StructureElement targetElement = null;
            foreach (StructureElement element in root.FindElements<StructureElement>(true))
            {
                if (element.ID == targetElementId)
                {
                    targetElement = element;
                    break;
                }
            }

            if (targetElement == null)
            {
                Console.Error.WriteLine($"Structure element with ID '{targetElementId}' not found.");
                return;
            }

            // Update the language to French (RFC 3066 tag)
            targetElement.Language = "fr-FR";

            // Save the modified PDF (PDF output, no special SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Language of element '{targetElementId}' set to French and saved to '{outputPath}'.");
    }
}