using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a new PDF document
        using (Document document = new Document())
        {
            // Add a page to the document
            Page page = document.Pages.Add();

            // Create a table with a single column of width 200 points
            Table table = new Table
            {
                ColumnWidths = "200"
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Allow the HTML fragment to override the cell's text state
            cell.IsOverrideByFragment = true;

            // Create an HTML fragment containing markup
            HtmlFragment htmlFragment = new HtmlFragment("<b>Bold Text</b> <i>Italic Text</i>");

            // Add the HTML fragment to the cell's paragraph collection
            cell.Paragraphs.Add(htmlFragment);

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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
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
