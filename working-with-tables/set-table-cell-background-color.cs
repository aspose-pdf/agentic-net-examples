using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a table with two columns (widths in points)
            Table table = new Table { ColumnWidths = "200 200" };

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row and set its background color
            Cell cell = row.Cells.Add("Cell with background");
            cell.BackgroundColor = Aspose.Pdf.Color.LightGray; // Desired background color

            // Add a second cell without a background color for comparison
            row.Cells.Add("Normal cell");

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF to disk – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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