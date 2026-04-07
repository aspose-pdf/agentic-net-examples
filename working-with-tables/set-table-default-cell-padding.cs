using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AsposePdfExamples
{
    class SetTableDefaultCellPadding
    {
        static void Main(string[] args)
        {
            // Create a new PDF document
            using (Document document = new Document())
            {
                // Add a page to the document
                Page page = document.Pages.Add();

                // Create a table and define column widths
                Table table = new Table();
                table.ColumnWidths = "100 100 100";

                // Define default cell padding for the entire table
                MarginInfo padding = new MarginInfo
                {
                    Top = 5f,
                    Bottom = 5f,
                    Left = 5f,
                    Right = 5f
                };
                table.DefaultCellPadding = padding;

                // Add a row with three cells
                Row row = table.Rows.Add();
                row.Cells.Add("Cell 1");
                row.Cells.Add("Cell 2");
                row.Cells.Add("Cell 3");

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Save the PDF document – guard against missing GDI+ on non‑Windows platforms
                string outputPath = "output.pdf";
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
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without GDI+ dependent features.");
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
