using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_fixed_row_height.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Example: set left and top coordinates
                Left = 50,
                Top = 700,
                // Define column widths (space‑separated values)
                ColumnWidths = "100 150 100"
            };

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Define a fixed row height (in points)
            const double fixedHeight = 30.0;

            // Add rows and set the FixedRowHeight before populating cells
            for (int i = 0; i < 5; i++)
            {
                // Create a new row
                Row row = table.Rows.Add();

                // Apply the fixed height to this row
                row.FixedRowHeight = fixedHeight;

                // Add three cells to the row and fill them with text
                for (int col = 0; col < 3; col++)
                {
                    // Create a text fragment for the cell content
                    TextFragment tf = new TextFragment($"R{i + 1}C{col + 1}")
                    {
                        // Optional styling
                        TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.Black }
                    };

                    // Add the cell with the text fragment
                    row.Cells.Add(tf);
                }
            }

            // Save the document – guard against missing GDI+ on non‑Windows platforms
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform; PDF saved without rendering the table.");
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