using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // For TextFragment

class Program
{
    static void Main()
    {
        const string outputPath = "vertical_alignment_table.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page, 1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a table with one column
            Table table = new Table
            {
                ColumnWidths = "200" // width of the single column
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // Create a cell and set its vertical alignment to Middle (Center)
            Cell cell = new Cell();
            cell.VerticalAlignment = VerticalAlignment.Center; // Middle alignment

            // Add some multi‑line text to demonstrate vertical centering
            TextFragment tf = new TextFragment("Line 1\nLine 2\nLine 3");
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            cell.Paragraphs.Add(tf);

            // Append the cell to the row
            row.Cells.Add(cell);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                                      "The PDF was not saved, but the program ran without crashing.");
                }
            }
        }
    }

    // Helper method to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
