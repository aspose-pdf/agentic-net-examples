using System;
using System.IO;
using System.Linq;
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
            // Check if the PDF is tagged by verifying TaggedContent is not null
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("The PDF is not tagged. No logical structure to modify.");
                return;
            }

            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root of the structure tree (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Find the first heading element (HeaderElement) in the tree
            HeaderElement heading = root.FindElements<HeaderElement>(true).FirstOrDefault();

            if (heading == null)
            {
                Console.WriteLine("No heading element found in the document.");
                return;
            }

            // Ensure the heading has at least one child to remove
            if (heading.ChildElements.Count == 0)
            {
                Console.WriteLine("The heading element has no child elements to remove.");
                return;
            }

            // Remove the first child from the heading using the index‑based overload
            heading.RemoveChild(0);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
