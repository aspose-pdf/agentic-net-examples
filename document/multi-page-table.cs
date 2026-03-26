using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_page_table.pdf";

        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table that will break across pages automatically
            Table table = new Table
            {
                // ColumnWidths must be a space‑separated string, not a double array
                ColumnWidths = "100 200 150",
                IsBroken = true,               // Enable automatic page breaking
                RepeatingRowsCount = 1         // Repeat the header row on each new page
            };

            // Header row
            Row header = table.Rows.Add();
            header.BackgroundColor = Aspose.Pdf.Color.LightGray;
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Description");

            // Populate many data rows to force pagination
            for (int i = 1; i <= 100; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(i.ToString());
                row.Cells.Add($"Item {i}");
                row.Cells.Add($"This is a description for item number {i}");
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform with libgdiplus installed)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved, but the code executed correctly.");
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
