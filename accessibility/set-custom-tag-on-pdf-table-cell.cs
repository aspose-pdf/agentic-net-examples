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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content of the document
            ITaggedContent tagged = doc.TaggedContent;

            // Ensure the document actually contains a structure tree
            if (tagged == null || tagged.RootElement == null)
            {
                Console.Error.WriteLine("The PDF does not contain a tagged structure.");
                return;
            }

            // Find all table cell (TD) elements in the structure tree
            var tableCells = tagged.RootElement.FindElements<TableTDElement>(true);

            if (tableCells.Count == 0)
            {
                Console.Error.WriteLine("No table cell elements were found in the PDF.");
                return;
            }

            // Select the first cell (or any specific cell you need)
            TableTDElement cell = tableCells[0];

            // Assign a custom tag to the selected cell – use SetTag method (Tag is a method, not a property)
            cell.SetTag("MyCustomCellTag");

            // Optionally set alternative text for accessibility
            cell.AlternativeText = "Table cell with custom tag";

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated table cell tag to '{outputPath}'.");
    }
}
