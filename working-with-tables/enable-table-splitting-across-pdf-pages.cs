using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_split.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and enable breaking across pages
            Table table = new Table
            {
                IsBroken = true,                     // Allow the table to split across pages
                ColumnWidths = "100 200 100"         // Define column widths (optional)
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("ID");
            header.Cells.Add("Description");
            header.Cells.Add("Value");

            // Populate the table with enough rows to require pagination
            for (int i = 1; i <= 100; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(i.ToString());
                row.Cells.Add($"Item number {i}");
                row.Cells.Add((i * 10).ToString());
            }

            // Add the table to the page's content
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing GDI+ on non‑Windows platforms
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
                                      "The PDF could not be saved.");
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
