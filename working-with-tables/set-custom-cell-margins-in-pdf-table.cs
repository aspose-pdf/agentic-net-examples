using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_margins.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a table; optionally set default cell padding
            Table table = new Table
            {
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5) // left, bottom, right, top
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell with some text
            Cell cell = row.Cells.Add("Cell with custom margins");

            // Configure custom margins for this cell using MarginInfo
            MarginInfo cellMargin = new MarginInfo
            {
                Left   = 10, // points from the left edge of the cell
                Right  = 10, // points from the right edge
                Top    = 5,  // points from the top edge
                Bottom = 5   // points from the bottom edge
            };
            cell.Margin = cellMargin;

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF. Guard against missing GDI+ on non‑Windows platforms.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("libgdiplus is required for PDF creation on this platform. Save skipped.");
                }
            }
            else
            {
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    static bool ContainsDllNotFound(Exception ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException) return true;
            ex = ex.InnerException;
        }
        return false;
    }
}