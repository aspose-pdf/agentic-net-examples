using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextState, FontStyles, BorderInfo, BorderSide

class Program
{
    static void Main()
    {
        const string outputPath = "alternating_rows.pdf";

        // Document lifecycle must be wrapped in a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three columns and a visible border
            Table table = new Table
            {
                ColumnWidths = "100 100 100",
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black)
            };
            page.Paragraphs.Add(table);

            // Header row (optional styling)
            Row header = table.Rows.Add();
            header.BackgroundColor = Aspose.Pdf.Color.LightGray;
            header.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                FontStyle = FontStyles.Bold
            };
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Value");

            // Add data rows and apply alternating background colors
            for (int i = 1; i <= 10; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(i.ToString());
                row.Cells.Add($"Item {i}");
                row.Cells.Add((i * 10).ToString());

                // Choose background color based on row index parity
                Aspose.Pdf.Color bgColor = (i % 2 == 0)
                    ? Aspose.Pdf.Color.FromRgb(0.9, 0.9, 0.9) // light gray for even rows
                    : Aspose.Pdf.Color.White;                // white for odd rows

                // Set the row background (optional, for visual consistency)
                row.BackgroundColor = bgColor;

                // Apply the same background color to each cell in the row
                foreach (Cell cell in row.Cells)
                {
                    cell.BackgroundColor = bgColor;
                }
            }

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to {outputPath}");
            }
            else
            {
                // On macOS/Linux the save may fail if libgdiplus is not installed.
                // Attempt to save and handle the possible GDI+ related exception gracefully.
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to {outputPath} (non‑Windows platform).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved.");
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
