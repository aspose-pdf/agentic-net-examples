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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Check if the document is tagged by verifying TaggedContent is not null
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. No structure to modify.");
                doc.Save(outputPath);
                return;
            }

            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Root element of the logical structure tree
            StructureElement root = taggedContent.RootElement;

            // Find the first ParagraphElement in the structure (recursive search)
            var paragraphs = root.FindElements<ParagraphElement>(true);
            if (paragraphs.Count > 0)
            {
                ParagraphElement paragraphToRemove = paragraphs[0];

                // Remove the selected element from its parent. Use Remove() on the element itself
                // because StructureElement.RemoveChild expects an index, not an element.
                paragraphToRemove.Remove();
                Console.WriteLine("Paragraph element removed successfully.");
            }
            else
            {
                Console.WriteLine("No paragraph elements found to remove.");
            }

            // Save the modified PDF (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
