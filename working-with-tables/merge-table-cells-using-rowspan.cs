using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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
                ColumnWidths = "100 100 100",
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };
            page.Paragraphs.Add(table);

            // First row
            Row row1 = table.Rows.Add();

            // Cell that will span two rows (merge with the cell directly below)
            Cell spanCell = row1.Cells.Add("Span Cell");
            spanCell.RowSpan = 2; // Set RowSpan to 2

            // Remaining cells in the first row
            row1.Cells.Add("R1C2");
            row1.Cells.Add("R1C3");

            // Second row (the first column is occupied by the spanned cell)
            Row row2 = table.Rows.Add();
            row2.Cells.Add("R2C2");
            row2.Cells.Add("R2C3");

            // Save the PDF document – guard against missing libgdiplus on non‑Windows platforms
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
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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