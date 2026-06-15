using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "merged_cells_table.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three columns
            Table table = new Table
            {
                // Define column widths (optional)
                ColumnWidths = "150 150 150"
            };

            // First row: a cell that spans all three columns
            Row firstRow = table.Rows.Add();
            Cell mergedCell = firstRow.Cells.Add("Merged Cell (ColSpan = 3)");
            mergedCell.ColSpan = 3; // Merge three adjacent columns into a single cell
            mergedCell.BackgroundColor = Color.LightGray;
            mergedCell.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                Font = FontRepository.FindFont("Helvetica"),
                ForegroundColor = Color.Black,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Second row: three regular cells
            Row secondRow = table.Rows.Add();
            secondRow.Cells.Add("Cell 1");
            secondRow.Cells.Add("Cell 2");
            secondRow.Cells.Add("Cell 3");

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with merged cells saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF with merged cells saved to '{outputPath}'. (Saved on non‑Windows platform)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF was not saved.");
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
