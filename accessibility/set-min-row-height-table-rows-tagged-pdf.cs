using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input_tagged.pdf";
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the PDF is tagged by checking the TaggedContent property
            if (doc.TaggedContent == null)
            {
                Console.Error.WriteLine("The PDF is not tagged. No structure tree to modify.");
                return;
            }

            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Root element of the logical structure tree
            StructureElement root = taggedContent.RootElement;

            // Find all table row elements (TableTRElement) in the structure tree
            var tableRows = root.FindElements<TableTRElement>(true);

            // Set a minimum row height for each table row
            foreach (TableTRElement row in tableRows)
            {
                // Height is expressed in points (1/72 inch). Adjust as needed.
                row.MinRowHeight = 12.0;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved with updated row heights to '{outputPath}'.");
    }
}
