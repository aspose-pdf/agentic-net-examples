using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_80_percent.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a table instance
            Table table = new Table();

            // Define the table width as 80 % of the page width by using a percentage column width.
            // Since the table has a single column, setting the column width to 80% makes the whole table occupy 80% of the page.
            table.ColumnWidths = "80%";

            // Center the table on the page (optional, improves visual layout)
            table.Alignment = HorizontalAlignment.Center;

            // Add a simple row with a cell containing some text
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add();
            cell.Paragraphs.Add(new TextFragment("This table occupies 80 % of the page width."));

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Guard Document.Save for platforms that may lack libgdiplus (e.g., macOS/Linux)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform; PDF saved without GDI+ dependent features.");
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
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
