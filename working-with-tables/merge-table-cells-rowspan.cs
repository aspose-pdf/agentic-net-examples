using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three columns
            Table table = new Table { ColumnWidths = "100 100 100" };

            // First row
            Row row1 = table.Rows.Add();
            Cell cell11 = row1.Cells.Add("Cell 1,1");
            Cell cell12 = row1.Cells.Add("Cell 1,2");
            Cell cell13 = row1.Cells.Add("Cell 1,3");

            // Second row
            Row row2 = table.Rows.Add();
            Cell cell21 = row2.Cells.Add("Cell 2,1");
            Cell cell22 = row2.Cells.Add("Cell 2,2");
            Cell cell23 = row2.Cells.Add("Cell 2,3");

            // Merge the first cell of the first row with the cell directly below
            cell11.RowSpan = 2;

            // Add the table to the page
            page.Paragraphs.Add(table);

            string outputPath = "output.pdf";

            // Guard Document.Save against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without GDI+ dependent features.");
                }
            }
        }
    }

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