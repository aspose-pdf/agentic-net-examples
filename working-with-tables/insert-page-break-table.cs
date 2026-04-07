using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AsposePdfExamples
{
    class InsertPageBreakAfterRow
    {
        static void Main(string[] args)
        {
            // Create a new PDF document
            using (Document doc = new Document())
            {
                // Add a page to the document
                Page page = doc.Pages.Add();

                // Create a table with two columns of equal width
                Table table = new Table();
                table.ColumnWidths = "200 200"; // widths in points

                // Add 10 rows; after the 5th row we force a page break
                for (int i = 1; i <= 10; i++)
                {
                    Row row = table.Rows.Add();

                    // First cell
                    Cell cell1 = row.Cells.Add();
                    TextFragment tf1 = new TextFragment($"Row {i}, Column 1");
                    cell1.Paragraphs.Add(tf1);

                    // Second cell
                    Cell cell2 = row.Cells.Add();
                    TextFragment tf2 = new TextFragment($"Row {i}, Column 2");
                    cell2.Paragraphs.Add(tf2);

                    // Force a page break after the 5th row
                    if (i == 5)
                    {
                        row.IsInNewPage = true;
                    }
                }

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Save the document – guard against missing GDI+ on non‑Windows platforms
                string outputPath = "output.pdf";
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
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                    }
                }
            }
        }

        private static bool ContainsDllNotFound(Exception ex)
        {
            Exception? current = ex;
            while (current != null)
            {
                if (current is DllNotFoundException)
                    return true;
                current = current.InnerException;
            }
            return false;
        }
    }
}
