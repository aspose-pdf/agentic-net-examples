using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "table_output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Table object
            Table table = new Table
            {
                // Optional: set table position and width
                Left = 50,
                Top = 700,
                ColumnWidths = "100 150 200" // three columns with specified widths
            };

            // ----- Add first (header) row -----
            Row row1 = table.Rows.Add(); // creates a new Row and adds it to the table
            // Populate cells in the first row
            row1.Cells.Add("Header 1");
            row1.Cells.Add("Header 2");
            row1.Cells.Add("Header 3");

            // Apply a simple style to the header row
            row1.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Color.White
            };
            // Set background colour for each cell (Row does not expose DefaultCellBackgroundColor)
            foreach (Cell cell in row1.Cells)
            {
                cell.BackgroundColor = Color.Gray;
            }

            // ----- Add second row -----
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Row 1, Col 1");
            row2.Cells.Add("Row 1, Col 2");
            row2.Cells.Add("Row 1, Col 3");

            // ----- Add third row -----
            Row row3 = table.Rows.Add();
            row3.Cells.Add("Row 2, Col 1");
            row3.Cells.Add("Row 2, Col 2");
            row3.Cells.Add("Row 2, Col 3");

            // Add the table to the page's paragraphs collection
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
