using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_colored_cell.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table, define column widths and optional border
            Table table = new Table
            {
                ColumnWidths = "100 100 100",
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };
            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row with some text
            Cell cell = row.Cells.Add("Cell with background");

            // Set the background color of the cell using Aspose.Pdf.Color
            cell.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was generated in memory but could not be saved to disk.");
                }
            }
        }
    }

    // Helper method that walks the inner‑exception chain looking for a DllNotFoundException
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
