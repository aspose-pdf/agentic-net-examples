using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "table_rowspan.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three equal-width columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100"
            };

            // ----- First row -----
            Row row1 = table.Rows.Add();

            // Cell that will span two rows (merge with the cell directly below)
            Cell mergedCell = row1.Cells.Add("Merged Cell");
            mergedCell.RowSpan = 2; // Set RowSpan to 2

            // Add the remaining cells for the first row
            row1.Cells.Add("Cell 1-2");
            row1.Cells.Add("Cell 1-3");

            // ----- Second row -----
            Row row2 = table.Rows.Add();

            // Since the first cell of the previous row spans two rows,
            // we only need to add two cells in this row.
            row2.Cells.Add("Cell 2-2");
            row2.Cells.Add("Cell 2-3");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the document – guard against missing GDI+ on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, GDI+ may be missing but save succeeded)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF could not be saved.");
                }
            }
        }
    }

    // Helper method to walk the inner‑exception chain and detect a missing native library
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
