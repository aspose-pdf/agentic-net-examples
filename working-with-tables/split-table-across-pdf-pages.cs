using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_split.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a table that will span multiple pages
            Table table = new Table
            {
                // Enable automatic splitting of the table across pages
                IsBroken = true,
                // Optional: set column widths (in points)
                ColumnWidths = "100 100 100"
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Column 1");
            header.Cells.Add("Column 2");
            header.Cells.Add("Column 3");

            // Populate the table with enough rows to force a page break
            for (int i = 1; i <= 200; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i} - Col 1");
                row.Cells.Add($"Row {i} - Col 2");
                row.Cells.Add($"Row {i} - Col 3");
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the document (Document.Save without SaveOptions writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with split table saved to '{outputPath}'.");
    }
}