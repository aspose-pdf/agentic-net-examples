using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "tables_autofit.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Table 1 – AutoFit to content (columns size to fit their text)
            // -------------------------------------------------
            Table table1 = new Table
            {
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
                Left = 50,   // X position on the page
                Top = 700    // Y position on the page
            };

            // Header row
            var headerRow1 = table1.Rows.Add();
            headerRow1.Cells.Add("Header 1");
            headerRow1.Cells.Add("Header 2");

            // Data row with varying text length
            var dataRow1 = table1.Rows.Add();
            dataRow1.Cells.Add("Short");
            dataRow1.Cells.Add("A much longer piece of text that will cause the column to expand");

            // Add the first table to the page
            page.Paragraphs.Add(table1);

            // -------------------------------------------------
            // Table 2 – AutoFit to window (columns expand to fill page width)
            // -------------------------------------------------
            Table table2 = new Table
            {
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow,
                Left = 50,
                Top = 500
            };

            var headerRow2 = table2.Rows.Add();
            headerRow2.Cells.Add("Col A");
            headerRow2.Cells.Add("Col B");
            headerRow2.Cells.Add("Col C");

            var dataRow2 = table2.Rows.Add();
            dataRow2.Cells.Add("Data 1");
            dataRow2.Cells.Add("Data 2");
            dataRow2.Cells.Add("Data 3");

            page.Paragraphs.Add(table2);

            // -------------------------------------------------
            // Table 3 – Customized column widths (explicit widths)
            // -------------------------------------------------
            Table table3 = new Table
            {
                ColumnAdjustment = ColumnAdjustment.Customized,
                ColumnWidths = "100 200 150", // widths in points for each column
                Left = 50,
                Top = 300
            };

            var headerRow3 = table3.Rows.Add();
            headerRow3.Cells.Add("Fixed 1");
            headerRow3.Cells.Add("Fixed 2");
            headerRow3.Cells.Add("Fixed 3");

            var dataRow3 = table3.Rows.Add();
            dataRow3.Cells.Add("Value A");
            dataRow3.Cells.Add("Value B");
            dataRow3.Cells.Add("Value C");

            page.Paragraphs.Add(table3);

            // -------------------------------------------------
            // Save the PDF document – guard against missing libgdiplus on non‑Windows platforms
            // -------------------------------------------------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Windows has GDI+ available – safe to save directly
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                // On macOS / Linux the native GDI+ library may be missing. Attempt to save and handle the possible failure gracefully.
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF was not saved.");
                }
            }
        }
    }

    // Helper that walks the exception chain looking for a DllNotFoundException (e.g., missing libgdiplus)
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
