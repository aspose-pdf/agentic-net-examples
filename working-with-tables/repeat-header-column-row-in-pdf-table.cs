using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // existing PDF (can be empty)
        const string outputPath = "output_with_table.pdf";

        // Ensure the input file exists; if not, create a blank PDF to host the table
        if (!File.Exists(inputPath))
        {
            using (Document blank = new Document())
            {
                blank.Pages.Add();
                blank.Save(inputPath);
            }
        }

        // Open the document, add a table with a header column that repeats on each page
        using (Document doc = new Document(inputPath))
        {
            // Create a new page if the document has none
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a table
            Table table = new Table
            {
                // Define column widths (example: two columns)
                ColumnWidths = "100 300",
                // Mark the first column as a repeating header column
                RepeatingColumnsCount = 1,
                // Optionally repeat the first row (header row) on each new page
                RepeatingRowsCount = 1,
                // Add some styling (optional)
                // The BorderInfo constructor overload without a Color argument is used to avoid the missing Aspose.Pdf.Drawing.Color type.
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Header row (will be repeated because RepeatingRowsCount = 1)
            Row headerRow = table.Rows.Add();
            // First cell – column header (will repeat because RepeatingColumnsCount = 1)
            Cell headerCell1 = headerRow.Cells.Add("Header Column");
            headerCell1.DefaultCellTextState = new TextState { FontSize = 12, FontStyle = FontStyles.Bold };
            // Second cell – regular header for the row
            Cell headerCell2 = headerRow.Cells.Add("Header Row");
            headerCell2.DefaultCellTextState = new TextState { FontSize = 12, FontStyle = FontStyles.Bold };

            // Add multiple data rows to force pagination
            for (int i = 1; i <= 100; i++)
            {
                Row dataRow = table.Rows.Add();
                // First column – will repeat on each new page
                dataRow.Cells.Add($"Item {i}");
                // Second column – regular data
                dataRow.Cells.Add($"Value {i}");
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the modified PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine($"PDF saved to '{outputPath}' (non‑Windows platform).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved using Aspose.Pdf's default renderer.");
                }
            }
        }
    }

    // Helper to detect a missing native GDI+ library deep in the exception chain
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
