using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with two columns
            Table table = new Table();
            table.ColumnWidths = "200 200";

            // Add many rows to force a page break
            for (int i = 1; i <= 30; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add("Row " + i + " Column 1");
                row.Cells.Add("Row " + i + " Column 2");
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Determine whether the table is broken across pages
            bool isBroken = table.IsBroken;
            Console.WriteLine("Is the table broken across pages? " + isBroken);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "output.pdf";
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (Non‑Windows platform, libgdiplus may be required)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved, but the program ran successfully.");
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
