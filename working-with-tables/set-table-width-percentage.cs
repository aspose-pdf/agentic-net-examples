using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

                // Create a table instance
                Table table = new Table
                {
                    // Position the table (optional)
                    Left = 0,
                    Top = 0,
                    // Set the table width to 80% of the page width by defining column width as 80%
                    ColumnWidths = "80%"
                };

                // Add a row with a single cell containing some text
                Row row = table.Rows.Add();
                Cell cell = row.Cells.Add("This table occupies 80% of the page width.");

                // Add the table to the page's paragraph collection
                page.Paragraphs.Add(table);

                // Save the resulting PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                        Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                    }
                }
            }
        }

        // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
        private static bool ContainsDllNotFound(Exception ex)
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
