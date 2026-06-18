using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a table instance
            Table table = new Table();

            // Offset the table from the left margin by 50 points
            // Table.Margin is a read‑only MarginInfo object; set its Left property directly
            table.Margin.Left = 50;

            // (Optional) Add a simple row with two cells for demonstration
            table.ColumnWidths = "200 200"; // two columns, each 200 points wide
            Row row = new Row();
            table.Rows.Add(row);
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Guard Document.Save on non‑Windows platforms where libgdiplus may be missing
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save("offset_table.pdf");
            }
            else
            {
                try
                {
                    doc.Save("offset_table.pdf");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform; PDF not saved.");
                }
            }
        }

        Console.WriteLine("PDF with offset table saved as 'offset_table.pdf'.");
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