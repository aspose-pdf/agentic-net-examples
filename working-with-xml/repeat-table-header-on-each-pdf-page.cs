using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_repeating_header.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and configure it
            Table table = new Table
            {
                // Define three equal‑width columns
                ColumnWidths = "100 100 100",
                // Repeat the first row (the header) on each new page
                RepeatingRowsCount = 1
            };

            // ----- Header row (will be repeated) -----
            Row header = table.Rows.Add();
            // Make header text bold
            header.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                FontStyle = FontStyles.Bold
            };
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // ----- Data rows (enough to span multiple pages) -----
            for (int i = 1; i <= 100; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Item {i}A");
                row.Cells.Add($"Item {i}B");
                row.Cells.Add($"Item {i}C");
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the document.
            // Guard against missing GDI+ (libgdiplus) on non‑Windows platforms.
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering graphics.");
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Helper method to detect a nested DllNotFoundException
    static bool ContainsDllNotFound(Exception ex)
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