using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "MultiPageTable.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page (first page is index 1)
            Page page = doc.Pages.Add();

            // Create a table that will automatically break across pages
            Table table = new Table();

            // Define column widths (values are in points)
            table.ColumnWidths = "100 200 100";

            // Enable automatic breaking of the table onto subsequent pages
            table.IsBroken = true;   // table will be split when it exceeds page height

            // Add a header row (will be repeated on each new page)
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");
            table.RepeatingRowsCount = 1; // repeat the first row as header

            // Populate the table with many rows to force pagination
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

        Console.WriteLine($"PDF with multi‑page table saved to '{outputPath}'.");
    }
}