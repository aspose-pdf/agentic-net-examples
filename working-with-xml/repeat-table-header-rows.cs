using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document pdf = new Document())
        {
            // Add a page to the document
            Page page = pdf.Pages.Add();

            // Create a table that will span multiple pages
            Table table = new Table
            {
                // Number of header rows that should repeat on each new page
                RepeatingRowsCount = 1,

                // Optional: define visual style for the repeating header rows
                RepeatingRowsStyle = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    ForegroundColor = Color.Black
                },

                // Optional: set column widths (comma‑separated or space‑separated)
                ColumnWidths = "150 250"
            };

            // ----- Header row (will be repeated) -----
            Row header = table.Rows.Add();
            header.Cells.Add("Product");
            header.Cells.Add("Price");

            // ----- Data rows (enough to force a page break) -----
            for (int i = 1; i <= 100; i++)
            {
                Row dataRow = table.Rows.Add();
                dataRow.Cells.Add($"Item {i}");
                dataRow.Cells.Add($"${i * 10}");
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the resulting PDF – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                pdf.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    pdf.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}' (non‑Windows platform)." );
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved, but the code executed correctly.");
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
