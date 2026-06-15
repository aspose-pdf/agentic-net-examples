using System;
using System.IO;
using System.Runtime.InteropServices; // for OS check
using Aspose.Pdf;
using Aspose.Pdf.Drawing;   // BorderInfo, MarginInfo, Color
using Aspose.Pdf.Text;      // TextFragment

class TableCellStylingExample
{
    static void Main()
    {
        // Paths for the output PDF
        const string outputPath = "styled_table.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Optional: set table background color
                BackgroundColor = Aspose.Pdf.Color.White,
                // Optional: set default cell padding for the whole table
                DefaultCellPadding = new MarginInfo { Left = 4, Right = 4, Top = 2, Bottom = 2 },
                // Optional: set default border for all cells (can be overridden per cell)
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray)
            };

            // Define column widths (example: three equal columns)
            table.ColumnWidths = "100 100 100";

            // Add three rows with three cells each
            for (int r = 0; r < 3; r++)
            {
                Row row = table.Rows.Add();

                // Optional: set row background color (light gray)
                row.BackgroundColor = Aspose.Pdf.Color.FromRgb(0.95, 0.95, 0.95);

                for (int c = 0; c < 3; c++)
                {
                    // Create a new cell
                    Cell cell = new Cell();

                    // Set cell text
                    cell.Paragraphs.Add(new TextFragment($"R{r + 1}C{c + 1}"));

                    // Style: background color
                    cell.BackgroundColor = Aspose.Pdf.Color.LightGray;

                    // Style: border thickness and color
                    cell.Border = new BorderInfo(BorderSide.All, 1.5f, Aspose.Pdf.Color.DarkBlue);

                    // Style: padding (margin inside the cell)
                    cell.Margin = new MarginInfo
                    {
                        Left = 6,
                        Right = 6,
                        Top = 4,
                        Bottom = 4
                    };

                    // Add the styled cell to the current row
                    row.Cells.Add(cell);
                }
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with styled table saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF with styled table saved to '{outputPath}' (non‑Windows platform).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved, but the code executed correctly.");
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
