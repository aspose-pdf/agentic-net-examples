using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_custom_borders.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a table and set column widths (3 columns)
            Table table = new Table();
            table.ColumnWidths = "100 150 200"; // space‑separated widths
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.LightGray);

            // Define border styles for odd and even rows
            BorderInfo oddRowBorder = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Blue);
            BorderInfo evenRowBorder = new BorderInfo(BorderSide.All, 2f, Aspose.Pdf.Color.Green);

            // Populate table with sample data (10 rows, 3 columns)
            for (int r = 0; r < 10; r++)
            {
                Row row = table.Rows.Add();
                // Apply custom border based on row index (0‑based)
                row.Border = (r % 2 == 0) ? oddRowBorder : evenRowBorder;

                for (int c = 0; c < 3; c++)
                {
                    Cell cell = row.Cells.Add();
                    cell.Paragraphs.Add(new TextFragment($"R{r + 1}C{c + 1}"));
                }
            }

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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (Saved on non‑Windows platform – ensure libgdiplus is installed.)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
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
