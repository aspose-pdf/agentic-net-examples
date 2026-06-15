using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF (core API)
        using (Document doc = new Document(inputPath))
        {
            // Create a simple table with three columns
            Table table = new Table
            {
                // Use customized adjustment so we can set explicit widths
                ColumnAdjustment = ColumnAdjustment.Customized
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A1");
            data.Cells.Add("Cell B1");
            data.Cells.Add("Cell C1");

            // Ensure the table has a defined width (optional, otherwise it uses page width)
            // Here we let Aspose.Pdf calculate the natural width based on content
            // and then convert each column width to a percentage of the total width.

            // Retrieve the total width of the table in points
            double totalWidth = table.GetWidth();

            // Collect the width of each column from the first row (all rows share column widths)
            var columnWidths = header.Cells
                                      .Select(cell => cell.Width) // Width is read‑only but populated after layout
                                      .ToArray();

            // If widths are zero (layout not performed yet), fall back to equal distribution
            if (columnWidths.Any(w => w == 0))
            {
                // Assign equal percentages
                int colCount = header.Cells.Count;
                string equalPercents = string.Join(" ", Enumerable.Repeat($"{100.0 / colCount:F2}%", colCount));
                table.ColumnWidths = equalPercents;
            }
            else
            {
                // Compute percentage for each column
                var percentages = columnWidths
                                  .Select(w => (w / totalWidth) * 100.0)
                                  .Select(p => $"{p:F2}%")
                                  .ToArray();

                // Set the ColumnWidths property using percentage strings
                table.ColumnWidths = string.Join(" ", percentages);
            }

            // Add the table to the first page
            doc.Pages[1].Paragraphs.Add(table);

            // Save the document.
            // Guard against missing GDI+ on non‑Windows platforms (macOS/Linux)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved may be incomplete.");
                }
            }
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
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