using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;   // for BorderSide enum and BorderInfo
using Aspose.Pdf.Text;      // for TextFragment

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "TableWithBorder.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document (first page is index 1)
            Page page = doc.Pages.Add();

            // Create a simple table with 2 columns and 2 rows
            Table table = new Table
            {
                // Position the table on the page
                Left = 50,
                Top = 700,
                // Set the column widths (optional)
                ColumnWidths = "200 200"
            };

            // ---- First row ----
            Row row1 = new Row();
            row1.Cells.Add(new Cell { Paragraphs = { new TextFragment("Cell 1") } });
            row1.Cells.Add(new Cell { Paragraphs = { new TextFragment("Cell 2") } });
            table.Rows.Add(row1);

            // ---- Second row ----
            Row row2 = new Row();
            row2.Cells.Add(new Cell { Paragraphs = { new TextFragment("Cell 3") } });
            row2.Cells.Add(new Cell { Paragraphs = { new TextFragment("Cell 4") } });
            table.Rows.Add(row2);

            // Configure the table border thickness to 2 points (set before rendering)
            table.Border = new BorderInfo(BorderSide.All, 2);

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                                      "The PDF was created without GDI+ dependent features.");
                }
            }
        }
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