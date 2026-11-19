using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "aligned_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (or create a new one if needed)
            Page page = doc.Pages[1];

            // Create a table and align it to the right margin
            Table table = new Table
            {
                HorizontalAlignment = HorizontalAlignment.Right // align right
            };

            // Optional: set padding for the table cells
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);

            // Define column widths (example: two columns of equal width)
            table.ColumnWidths = "100 100";

            // Add a row with two cells
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");

            // Add the table to the page's content
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table aligned to right saved to '{outputPath}'.");
    }
}
