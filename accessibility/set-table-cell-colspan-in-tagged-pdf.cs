using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "tagged_input.pdf";
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document is tagged by checking TaggedContent for null
            if (doc.TaggedContent == null)
            {
                Console.Error.WriteLine("The PDF is not tagged. Operation aborted.");
                return;
            }

            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root structure element (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Find the first table cell element (TH or TD) in the structure tree
            // TableCellElement is the base type for TableTHElement and TableTDElement
            var cells = root.FindElements<TableCellElement>(true);
            if (cells.Count == 0)
            {
                Console.Error.WriteLine("No table cell elements found in the tagged PDF.");
                return;
            }

            // Example: set column span on the first cell found
            TableCellElement firstCell = cells[0];
            firstCell.ColSpan = 2; // Span across two columns

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved with updated ColSpan to '{outputPath}'.");
    }
}
