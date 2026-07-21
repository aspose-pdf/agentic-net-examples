using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output paths (adjust as needed)
        const string outputPath = "table_width_demo.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Position the table (left, top) – values are in points (1/72 inch)
                // Here we place it 50 points from the left and 700 points from the bottom
                // (Aspose.Pdf uses bottom‑left origin for page coordinates)
                // The Width is not set explicitly; it will be calculated after layout
                // based on column definitions and cell contents.
                // The Table will be added to the page's Paragraphs collection.
                // No need to set Width property directly.
                // The GetWidth() method will return the calculated width after layout.
                // Example column widths: three columns with relative widths
                ColumnWidths = "33 33 34"
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a few data rows
            for (int i = 1; i <= 5; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i} - Col 1");
                row.Cells.Add($"Row {i} - Col 2");
                row.Cells.Add($"Row {i} - Col 3");
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Force layout by saving the document (or by calling doc.Save with a stream)
            // The layout engine runs during Save, after which GetWidth() returns the
            // actual rendered width.
            doc.Save(outputPath);

            // After layout, retrieve the calculated width of the table
            double renderedWidth = table.GetWidth();

            // Output the width to the console
            Console.WriteLine($"Rendered table width: {renderedWidth} points");
        }
    }
}