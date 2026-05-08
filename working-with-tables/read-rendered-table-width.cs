using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for BorderInfo

class Program
{
    static void Main()
    {
        const string outputPath = "table_output.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three columns; column widths will be auto‑calculated after layout
            Table table = new Table
            {
                // Optional: set a default border for visual clarity
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // First row
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Cell 1");
            row1.Cells.Add("Cell 2");
            row1.Cells.Add("Cell 3");

            // Second row
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Another 1");
            row2.Cells.Add("Another 2");
            row2.Cells.Add("Another 3");

            // Add the table to the page at the desired position
            page.Paragraphs.Add(table);

            // After the table is added to the page the layout engine calculates its size.
            // The rendered width can be obtained by parsing the calculated column widths string.
            double renderedWidth = ParseColumnWidths(table.ColumnWidths).Sum();
            Console.WriteLine($"Rendered table width: {renderedWidth}");

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms.
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus).
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

    // Parses the Table.ColumnWidths string (e.g., "50 100 150") into a sequence of doubles.
    private static IEnumerable<double> ParseColumnWidths(string columnWidths)
    {
        if (string.IsNullOrWhiteSpace(columnWidths))
            return Enumerable.Empty<double>();

        // Split by spaces or commas, remove empty entries, and parse each token.
        return columnWidths
            .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(token =>
            {
                // Use InvariantCulture to ensure '.' as decimal separator.
                if (double.TryParse(token, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double value))
                    return value;
                // If parsing fails, treat the width as 0 to avoid exceptions.
                return 0.0;
            });
    }
}
