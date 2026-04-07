using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

namespace TableExample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Document document = new Document())
            {
                // Add a new page to the document
                Page page = document.Pages.Add();

                // Create a table with three columns
                Table table = new Table();
                table.ColumnWidths = "100 100 100"; // three equal columns

                // Define a default border for all cells without using System.Drawing.Color
                // (using the constructor that does not require a Color avoids GDI+ dependency)
                BorderInfo defaultBorder = new BorderInfo(BorderSide.All, 1f);
                table.DefaultCellBorder = defaultBorder;

                // Add a header row
                Row headerRow = table.Rows.Add();
                headerRow.Cells.Add("Header 1");
                headerRow.Cells.Add("Header 2");
                headerRow.Cells.Add("Header 3");

                // Add three data rows
                for (int i = 1; i <= 3; i++)
                {
                    Row dataRow = table.Rows.Add();
                    dataRow.Cells.Add($"Row {i} Col 1");
                    dataRow.Cells.Add($"Row {i} Col 2");
                    dataRow.Cells.Add($"Row {i} Col 3");
                }

                // Add the table to the page
                page.Paragraphs.Add(table);

                string outputPath = "output.pdf";

                // Guard Document.Save for platforms where libgdiplus (GDI+) is missing
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    document.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                else
                {
                    try
                    {
                        document.Save(outputPath);
                        Console.WriteLine($"PDF saved to '{outputPath}'.");
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
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
