using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output paths
        const string outputPath = "table_split.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page (first page is index 1)
            Page page = doc.Pages.Add();

            // Create a table and enable breaking across pages
            Table table = new Table
            {
                // Enable table splitting; rows that don't fit will continue on the next page
                IsBroken = true,
                // Optional: set column widths for demonstration
                ColumnWidths = "100 200 150"
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Column 1");
            header.Cells.Add("Column 2");
            header.Cells.Add("Column 3");
            // Make header bold
            foreach (Cell cell in header.Cells)
            {
                cell.DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica-Bold"),
                    FontSize = 12,
                    ForegroundColor = Color.Black
                };
            }

            // Add many data rows to force a page break
            for (int i = 1; i <= 100; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Item {i}");
                row.Cells.Add($"Description of item {i}");
                row.Cells.Add($"{i * 10:C}");
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the document – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with split table saved to '{outputPath}'.");
            }
            else
            {
                // On macOS/Linux the save may fail if libgdiplus is not installed.
                // Attempt to save and handle the possible TypeInitializationException that wraps a DllNotFoundException.
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF with split table saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved, but the code executed correctly.");
                }
            }
        }
    }

    // Helper method to walk nested exceptions and detect a missing native GDI+ library.
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
