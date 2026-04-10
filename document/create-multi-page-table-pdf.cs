using System;
using Aspose.Pdf;

// NOTE: To avoid the runtime "System.Private.Windows.Core" assembly load error,
// ensure the project targets a Windows‑specific TFM (e.g., net6.0-windows) or add the
// "Microsoft.Windows.Compatibility" NuGet package. No code changes are required beyond
// this comment, but the project file must reflect the appropriate target framework.

class Program
{
    static void Main()
    {
        const string outputPath = "multi_page_table.pdf";

        // Document lifecycle must be wrapped in a using block (document-disposal-with-using)
        using (Document doc = new Document())
        {
            // Add the first page (page-indexing-one-based rule applies to existing pages)
            Page page = doc.Pages.Add();

            // Create a table that can break across pages automatically
            Table table = new Table
            {
                // Enable automatic breaking of the table onto subsequent pages
                IsBroken = true,
                // Repeat the first row (header) on each new page
                RepeatingRowsCount = 1,
                // Define column widths (optional, space‑separated values)
                ColumnWidths = "80 200 120"
            };

            // Header row (will be repeated on each page)
            Row header = table.Rows.Add();
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Value");

            // Populate many rows to force the table to span multiple pages
            for (int i = 1; i <= 200; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(i.ToString());
                row.Cells.Add($"Item {i}");
                row.Cells.Add((i * 10).ToString());
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the document as PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑page table saved to '{outputPath}'.");
    }
}
