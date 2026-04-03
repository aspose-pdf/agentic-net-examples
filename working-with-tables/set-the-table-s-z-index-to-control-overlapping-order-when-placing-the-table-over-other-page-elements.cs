using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table and configure its appearance
            Table table = new Table
            {
                // Position the table on the page
                Left   = 100,   // X coordinate
                Top    = 500,   // Y coordinate
                // Set Z‑index: larger value means the table is drawn over lower‑Z elements
                ZIndex = 10
            };

            // Define column widths (optional)
            table.ColumnWidths = "100 150";

            // Add a row with two cells
            Row row = table.Rows.Add();
            Cell cell1 = row.Cells.Add();
            Cell cell2 = row.Cells.Add();

            // Populate cells with text
            cell1.Paragraphs.Add(new TextFragment("Cell 1"));
            cell2.Paragraphs.Add(new TextFragment("Cell 2"));

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with ZIndex added and saved to '{outputPath}'.");
    }
}