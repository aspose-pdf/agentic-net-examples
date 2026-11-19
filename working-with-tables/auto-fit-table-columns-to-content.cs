using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for text handling if needed

class Program
{
    static void Main()
    {
        const string outputPath = "table_autofit.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a table and set ColumnAdjustment to AutoFitToContent for auto‑sizing columns
            Table table = new Table
            {
                ColumnAdjustment = Aspose.Pdf.ColumnAdjustment.AutoFitToContent,
                // ColumnWidths can be set but will be adjusted automatically by the auto‑fit behavior
                ColumnWidths = "100 150 200"
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Data row with varying text lengths to demonstrate autofit
            Row data = table.Rows.Add();
            data.Cells.Add("Short");
            data.Cells.Add("This is a much longer piece of text that should cause the column to expand automatically.");
            data.Cells.Add("Medium length");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
