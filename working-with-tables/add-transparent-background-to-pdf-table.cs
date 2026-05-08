using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // Table resides in Aspose.Pdf namespace, but drawing helpers are useful

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_background.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position
            Table table = new Table
            {
                // Position the table (optional)
                Left = 50,
                Top = 700,
                // Set the background color with desired opacity (e.g., 50% transparent red)
                // Aspose.Pdf.Color.FromArgb(alpha, red, green, blue) where alpha 0-255
                BackgroundColor = Aspose.Pdf.Color.FromArgb(128, 255, 0, 0)
            };

            // Define column widths (optional)
            table.ColumnWidths = "100 150";

            // Add a header row
            Row header = table.Rows.Add();
            header.BackgroundColor = Aspose.Pdf.Color.LightGray; // distinct header background
            header.Cells.Add("Product");
            header.Cells.Add("Price");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Widget A");
            data.Cells.Add("$25.00");

            // Add the table to the page's paragraphs collection
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved using Aspose.Pdf's default renderer.");
                    // Optionally, you could implement an alternative saving strategy here.
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
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
