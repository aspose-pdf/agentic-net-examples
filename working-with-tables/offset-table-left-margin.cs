using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace TableOffsetExample
{
    class Program
    {
        static void Main(string[] args)
        {
            const string outputPath = "output.pdf";

            // Create a new PDF document
            using (Document document = new Document())
            {
                // Add a page to the document
                Page page = document.Pages.Add();

                // Create a table and offset it from the left margin
                Table table = new Table();
                table.Margin = new MarginInfo();
                table.Margin.Left = 50.0; // offset 50 points from the left edge

                // Define column widths
                table.ColumnWidths = "100 100 100";

                // Add a row with three cells
                Row row = table.Rows.Add();
                row.Cells.Add("Cell 1");
                row.Cells.Add("Cell 2");
                row.Cells.Add("Cell 3");

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    document.Save(outputPath);
                }
                else
                {
                    try
                    {
                        document.Save(outputPath);
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                          "The PDF was not saved using GDI+ dependent features.");
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
}
