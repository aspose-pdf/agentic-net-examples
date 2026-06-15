using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_fixed_width.pdf";

        // Document lifecycle must be wrapped in a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a table instance with a fixed total width of 500 points
            Table table = new Table();
            table.ColumnWidths = "500"; // single column width defines the whole table width

            // Add a single row with one cell containing sample text
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add();
            cell.Paragraphs.Add(new TextFragment("This table has a fixed width of 500 points."));

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Guard Document.Save for platforms that may lack libgdiplus (e.g., macOS/Linux)
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

    // Helper to detect a nested DllNotFoundException caused by missing libgdiplus
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