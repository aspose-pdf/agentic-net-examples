using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // needed for TextFragment

class TableCellStylingExample
{
    static void Main()
    {
        const string outputPath = "styled_table.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position
            Table table = new Table
            {
                ColumnWidths = "100 150 200", // three columns with specific widths
                DefaultCellPadding = new MarginInfo { Left = 5, Right = 5, Top = 5, Bottom = 5 },
                // BorderInfo constructor includes color; no .Color property
                DefaultCellBorder = new BorderInfo(BorderSide.All, 1f, Color.Black)
            };
            page.Paragraphs.Add(table);

            // Define a border style to be applied to each cell (width 2, dark blue)
            BorderInfo cellBorder = new BorderInfo(BorderSide.All, 2f, Color.DarkBlue);

            // Define padding for each cell
            MarginInfo cellPadding = new MarginInfo { Left = 8, Right = 8, Top = 4, Bottom = 4 };

            // Add rows and cells
            for (int r = 0; r < 4; r++)
            {
                Row row = table.Rows.Add();

                for (int c = 0; c < 3; c++)
                {
                    // Create a new cell
                    Cell cell = new Cell();

                    // Set cell content
                    cell.Paragraphs.Add(new TextFragment($"R{r + 1}C{c + 1}"));

                    // Apply visual styling
                    cell.BackgroundColor = Color.LightGray;
                    cell.Border = cellBorder;
                    cell.Margin = cellPadding;

                    // Add the cell to the current row
                    row.Cells.Add(cell);
                }
            }

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
                    Console.WriteLine($"PDF with styled table saved to '{outputPath}' (non‑Windows platform)." );
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: libgdiplus (GDI+) is not available on this platform. " +
                                      "The PDF could not be saved using Aspose.Pdf's rendering engine.");
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
