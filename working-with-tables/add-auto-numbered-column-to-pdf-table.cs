using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_numbers.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three columns
            Table table = new Table
            {
                // Define column widths (first column for numbers)
                ColumnWidths = "50 150 150",
                // Optional styling for cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Define how many rows the table will have
            int rowCount = 10;

            // Populate the table
            for (int i = 0; i < rowCount; i++)
            {
                // Add a new row
                Row row = table.Rows.Add();

                // First cell: sequential number (1‑based)
                TextFragment numberFragment = new TextFragment((i + 1).ToString());
                row.Cells.Add(numberFragment);

                // Additional sample cells
                row.Cells.Add(new TextFragment($"Data A{i + 1}"));
                row.Cells.Add(new TextFragment($"Data B{i + 1}"));
            }

            // Add the completed table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"Saved PDF with auto‑numbered column to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                // Optionally, you could implement an alternative saving strategy here.
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException (e.g., libgdiplus)
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
