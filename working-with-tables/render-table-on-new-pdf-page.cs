using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_page.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a fresh page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a table and configure its layout
            Table table = new Table
            {
                // Define three column widths (in points)
                ColumnWidths = "100 150 100",
                // Apply a thin black border to all cells by default
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                // Add padding inside each cell
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // ----- Header row -----
            Row header = table.Rows.Add();
            Cell h1 = header.Cells.Add("Product");
            Cell h2 = header.Cells.Add("Quantity");
            Cell h3 = header.Cells.Add("Price");

            // Style header cells (bold white text on gray background)
            foreach (Cell c in header.Cells)
            {
                c.DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica-Bold"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.White
                };
                c.BackgroundColor = Aspose.Pdf.Color.Gray;
            }

            // ----- Data rows -----
            Row data1 = table.Rows.Add();
            data1.Cells.Add("Widget A");
            data1.Cells.Add("10");
            data1.Cells.Add("$25");

            Row data2 = table.Rows.Add();
            data2.Cells.Add("Widget B");
            data2.Cells.Add("5");
            data2.Cells.Add("$40");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with table saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF with table saved to '{outputPath}'. (Saved on non‑Windows platform – ensure libgdiplus is installed if you encounter runtime issues.)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF could not be saved.");
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
