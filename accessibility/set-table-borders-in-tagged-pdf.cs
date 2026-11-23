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
            // Access the tagged content of the document
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Find all TableElement instances in the structure (recursive search)
            var tables = root.FindElements<TableElement>(true);

            // Set a border on each table
            foreach (TableElement table in tables)
            {
                // Use the BorderInfo constructor: BorderSide, width (float), and color
                table.Border = new BorderInfo(BorderSide.All, 1f, Color.Black);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
