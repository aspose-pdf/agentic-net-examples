using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // BorderSide enum lives here

class Program
{
    static void Main()
    {
        const string outputPath = "styled_table.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Default border using the proper constructor (BorderSide, width, color)
            var defaultBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray);

            // Create a table with three equal-width columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100",
                // Default styling applied to all cells unless overridden
                DefaultCellPadding = new MarginInfo { Left = 5, Right = 5, Top = 5, Bottom = 5 },
                DefaultCellBorder = defaultBorder
            };

            // Populate the table with rows and cells
            for (int r = 0; r < 3; r++)
            {
                Row row = table.Rows.Add();
                for (int c = 0; c < 3; c++)
                {
                    // Create a cell with text content
                    Cell cell = new Cell
                    {
                        // Add text to the cell (TextFragment lives in Aspose.Pdf.Text)
                        Paragraphs = { new TextFragment($"R{r + 1}C{c + 1}") },

                        // Cell-specific background color
                        BackgroundColor = Color.LightYellow,

                        // Cell-specific border (use constructor, overrides default if needed)
                        Border = new BorderInfo(BorderSide.All, 1f, Color.DarkBlue),

                        // Cell-specific padding (margin)
                        Margin = new MarginInfo { Left = 4, Right = 4, Top = 2, Bottom = 2 }
                    };

                    row.Cells.Add(cell);
                }
            }

            // Add the styled table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing GDI+ on non‑Windows platforms
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
                                      "The PDF could not be saved using Aspose.Pdf's default renderer.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException (e.g., libgdiplus)
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
