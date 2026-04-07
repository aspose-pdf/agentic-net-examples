using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new PDF document
            using (Document doc = new Document())
            {
                // Add a page to the document
                Page page = doc.Pages.Add();

                // Create a table and disable automatic splitting
                Table table = new Table();
                table.ColumnWidths = "100 100 100";
                table.IsBroken = false; // Prevent the table from breaking across pages

                // Add a header row
                Row header = table.Rows.Add();
                header.Cells.Add("Header 1");
                header.Cells.Add("Header 2");
                header.Cells.Add("Header 3");

                // Add many rows to exceed a page height
                for (int i = 0; i < 30; i++)
                {
                    Row row = table.Rows.Add();
                    row.Cells.Add($"Row {i} Col 1");
                    row.Cells.Add($"Row {i} Col 2");
                    row.Cells.Add($"Row {i} Col 3");
                }

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
                string outputPath = "output.pdf";
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
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
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
