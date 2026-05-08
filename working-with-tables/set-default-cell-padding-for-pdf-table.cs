using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text; // optional, kept for completeness

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_padding.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a table with 3 columns and 4 rows and set default cell padding
            Table table = new Table
            {
                ColumnWidths = "100 100 100",
                DefaultCellPadding = new MarginInfo
                {
                    Left = 5,
                    Right = 5,
                    Top = 5,
                    Bottom = 5
                }
            };

            // Populate the table with sample data
            for (int r = 0; r < 4; r++)
            {
                Row row = table.Rows.Add();
                for (int c = 0; c < 3; c++)
                {
                    Cell cell = row.Cells.Add($"R{r + 1}C{c + 1}");
                    // Optional: set a border to visualize the padding
                    cell.Border = new BorderInfo(BorderSide.All, 0.5f);
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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

    // Helper to detect a nested DllNotFoundException (libgdiplus) inside TypeInitializationException
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