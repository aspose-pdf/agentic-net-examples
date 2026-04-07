using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            const string outputPath = "output.pdf";

            // Create a new PDF document
            using (Document doc = new Document())
            {
                // Add a page to the document
                Page page = doc.Pages.Add();

                // ------------------------------------------------------------
                // First table – let the content determine column widths (auto‑fit)
                // ------------------------------------------------------------
                Table table1 = new Table();
                // No explicit AutoFitBehavior property – simply omit ColumnWidths
                // to let Aspose.Pdf size columns based on cell content.
                table1.DefaultCellBorder = new BorderInfo(BorderSide.All);
                // Add a row with three cells
                Row row1 = table1.Rows.Add();
                row1.Cells.Add("Cell 1A");
                row1.Cells.Add("Cell 1B");
                row1.Cells.Add("Cell 1C");
                // Position the first table on the page
                table1.Top = 100f;
                table1.Left = 50f;
                page.Paragraphs.Add(table1);

                // ------------------------------------------------------------
                // Second table – use fixed column widths
                // ------------------------------------------------------------
                Table table2 = new Table();
                // Define explicit column widths (fixed‑width behavior)
                table2.ColumnWidths = "50 150 200";
                table2.DefaultCellBorder = new BorderInfo(BorderSide.All);
                Row row2 = table2.Rows.Add();
                row2.Cells.Add("Cell 2A");
                row2.Cells.Add("Cell 2B");
                row2.Cells.Add("Cell 2C");
                // Position the second table lower on the page
                table2.Top = 300f;
                table2.Left = 50f;
                page.Paragraphs.Add(table2);

                // ------------------------------------------------------------
                // Save the resulting PDF – guard against missing libgdiplus on non‑Windows platforms
                // ------------------------------------------------------------
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
                        Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
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
}
