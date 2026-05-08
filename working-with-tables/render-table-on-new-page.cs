using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class RenderTableOnNewPage
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a new blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a table instance
            Table table = new Table();

            // Define three equal column widths (in points)
            table.ColumnWidths = "150 150 150";

            // ----- First row (header) -----
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");
            headerRow.Cells.Add("Header 3");

            // ----- Second row (data) -----
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Cell A1");
            dataRow.Cells.Add("Cell A2");
            dataRow.Cells.Add("Cell A3");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "RenderedTable.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                // On macOS/Linux Aspose.Pdf may require libgdiplus for saving.
                // Either install libgdiplus or skip saving to avoid a crash.
                Console.WriteLine("Skipping doc.Save(): libgdiplus is required on this platform to save PDFs.");
            }
        }
    }
}
