using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

public class TableSplitExample
{
    public static void Main()
    {
        // Create a new PDF document
        using (Document document = new Document())
        {
            // Add a page to the document
            Page page = document.Pages.Add();

            // Create a table and enable splitting across pages
            Table table = new Table();
            table.ColumnWidths = "100 100 100";
            table.IsBroken = true; // Allow the table to break onto the next page when needed

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add many rows to force a page break
            for (int i = 0; i < 50; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i} Col 1");
                row.Cells.Add($"Row {i} Col 2");
                row.Cells.Add($"Row {i} Col 3");
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                document.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    document.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, libgdiplus may be required)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF was not saved.");
                }
            }
        }
    }

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