using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Globalization;

class Program
{
    static void Main()
    {
        // Define the minimum column width in points (e.g., 50 points)
        const double minColumnWidth = 50.0;

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its default column width to enforce the minimum width
            Table table = new Table
            {
                // Aspose.Pdf.Table.DefaultColumnWidth expects a string, so convert the double value.
                DefaultColumnWidth = minColumnWidth.ToString(CultureInfo.InvariantCulture)
            };

            // Optionally, define explicit column widths (you can leave this out to rely on DefaultColumnWidth)
            // table.ColumnWidths = "100 150 200";

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add(CreateCell("Header 1"));
            header.Cells.Add(CreateCell("Header 2"));
            header.Cells.Add(CreateCell("Header 3"));

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add(CreateCell("Cell A1"));
            data.Cells.Add(CreateCell("Cell A2"));
            data.Cells.Add(CreateCell("Cell A3"));

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF to a file – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            const string outputPath = "output_min_column_width.pdf";
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, GDI+ may be missing but save succeeded)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF could not be saved.");
                }
            }
        }
    }

    // Helper method to create a cell with simple text
    private static Cell CreateCell(string text)
    {
        // Create a text fragment for the cell content
        TextFragment tf = new TextFragment(text);
        // Create a cell and add the text fragment to its paragraphs
        Cell cell = new Cell();
        cell.Paragraphs.Add(tf);
        return cell;
    }

    // Recursively checks inner exceptions for a DllNotFoundException (e.g., missing libgdiplus)
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
