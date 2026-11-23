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
            // Verify that the document is tagged by checking TaggedContent (IsTagged does not exist)
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. No structure to modify.");
                doc.Save(outputPath);
                return;
            }

            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Find the first table cell (TableTDElement) in the structure tree
            TableTDElement targetCell = null;
            var cells = taggedContent.RootElement.FindElements<TableTDElement>(true);
            if (cells.Count > 0)
                targetCell = cells[0];

            if (targetCell != null)
            {
                // Set the RowSpan property (example: span 2 rows)
                targetCell.RowSpan = 2;
                Console.WriteLine("RowSpan set on the first table cell.");
            }
            else
            {
                Console.WriteLine("No TableTDElement found in the document.");
            }

            // Save the modified PDF; no PreSave call is required
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
