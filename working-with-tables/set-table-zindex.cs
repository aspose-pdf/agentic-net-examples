using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "table_zindex.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (page indexing is 1‑based)
            Page page = doc.Pages.Add();

            // Create a table that will be placed on the page
            Table table = new Table();

            // Position the table on the page
            table.Left = 100;   // X coordinate
            table.Top  = 500;   // Y coordinate

            // Add a simple row with two cells
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");

            // Set ZIndex – higher values are drawn over lower values
            table.ZIndex = 10;

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF (Document.Save without SaveOptions always writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}