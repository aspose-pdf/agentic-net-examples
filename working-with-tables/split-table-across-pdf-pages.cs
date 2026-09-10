using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output paths (adjust as needed)
        const string outputPath = "table_split.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a page (first page is index 1)
            Page page = doc.Pages.Add();

            // Create a table that will be split across pages
            Table table = new Table
            {
                // Enable table breaking so it can continue on the next page
                IsBroken = true,

                // Optional visual settings
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Define column widths (example: three equal columns)
            table.ColumnWidths = "100 100 100";

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");
            // Mark header row as repeated on each split page (optional)
            header.IsInNewPage = false; // keep on same page as table start

            // Populate many rows to force the table to span multiple pages
            for (int i = 1; i <= 100; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i} - Col 1");
                row.Cells.Add($"Row {i} - Col 2");
                row.Cells.Add($"Row {i} - Col 3");
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with split table saved to '{outputPath}'.");
    }
}