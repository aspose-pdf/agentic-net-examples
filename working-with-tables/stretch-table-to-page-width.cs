using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // For text fragments (optional)

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "table_autofit.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set it to stretch across the page width
            Table table = new Table
            {
                // Use ColumnAdjustment to make the table fit the page width
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow,

                // Optional: set table position and margins
                Left = 0,
                Top = 0
            };

            // Define three columns (widths will be auto‑adjusted and stretched to the page width)
            table.ColumnWidths = "100 100 100";

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell 1");
            data.Cells.Add("Cell 2");
            data.Cells.Add("Cell 3");

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
