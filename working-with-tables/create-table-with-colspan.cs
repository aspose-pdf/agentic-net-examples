using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextState, if needed

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page, 1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a table with three columns
            Table table = new Table
            {
                // Define equal column widths (you can adjust as needed)
                ColumnWidths = "150 150 150"
            };

            // ---------- First row: a merged cell spanning three columns ----------
            Row mergedRow = table.Rows.Add();                     // Add a new row
            Cell mergedCell = mergedRow.Cells.Add("Merged Cell"); // Add a cell with text
            mergedCell.ColSpan = 3;                               // Merge three adjacent columns

            // Optional: set some visual styling for the merged cell
            mergedCell.BackgroundColor = Color.LightGray;
            mergedCell.DefaultCellTextState = new TextState
            {
                FontSize = 14,
                Font = FontRepository.FindFont("Helvetica"),
                ForegroundColor = Color.Black,
                FontStyle = FontStyles.Bold
            };

            // ---------- Second row: regular three separate cells ----------
            Row normalRow = table.Rows.Add();
            normalRow.Cells.Add("Cell 1");
            normalRow.Cells.Add("Cell 2");
            normalRow.Cells.Add("Cell 3");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            string outputPath = "TableWithColSpan.pdf";

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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved using Aspose.Pdf's rendering engine.");
                }
            }
        }

        Console.WriteLine("PDF creation routine finished.");
    }

    // Helper method to walk the inner‑exception chain and detect a missing native library
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
