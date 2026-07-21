using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_split.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to host the table
            Page page = doc.Pages.Add();

            // Create a table, enable splitting across pages, and define column widths
            Table table = new Table
            {
                IsBroken = true,                         // Allow the table to break across pages
                ColumnWidths = "100 100 100",            // Three equal columns
                DefaultCellBorder = new BorderInfo(     // Simple border for all cells
                    BorderSide.All,
                    0.5f,                               // float literal as required
                    Color.Black)
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Populate many rows to force the table to span multiple pages
            for (int i = 1; i <= 100; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Item {i}A");
                row.Cells.Add($"Item {i}B");
                row.Cells.Add($"Item {i}C");
            }

            // Insert the table into the page's content
            page.Paragraphs.Add(table);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with split table saved to '{outputPath}'.");
    }
}
