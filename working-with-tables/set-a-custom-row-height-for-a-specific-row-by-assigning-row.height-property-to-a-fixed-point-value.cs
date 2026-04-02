using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "TableWithCustomRowHeight.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page (page indexing is 1‑based)
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table
            {
                // Simple column layout: two columns of equal width
                ColumnWidths = "200 200",
                // Optional: set a border for the whole table
                Border = new BorderInfo(BorderSide.All, 1f)
            };
            page.Paragraphs.Add(table);

            // ----- Row 1 (header) -----
            Row headerRow = table.Rows.Add();
            headerRow.BackgroundColor = Color.LightGray;
            // Set a fixed height for the header row (e.g., 30 points)
            headerRow.FixedRowHeight = 30;
            // Add cells to the header row
            Cell headerCell1 = headerRow.Cells.Add("Header 1");
            Cell headerCell2 = headerRow.Cells.Add("Header 2");
            // Optional styling for header cells
            headerCell1.DefaultCellTextState = new TextState { FontSize = 12, FontStyle = FontStyles.Bold };
            headerCell2.DefaultCellTextState = new TextState { FontSize = 12, FontStyle = FontStyles.Bold };

            // ----- Row 2 (custom height) -----
            Row customRow = table.Rows.Add();
            // Set a custom fixed height (e.g., 80 points)
            customRow.FixedRowHeight = 80;
            // Add cells to the custom-height row
            Cell customCell1 = customRow.Cells.Add("Tall Row Cell 1");
            Cell customCell2 = customRow.Cells.Add("Tall Row Cell 2");
            // Example: wrap text to demonstrate the height effect
            customCell1.DefaultCellTextState = new TextState { FontSize = 10 };
            customCell2.DefaultCellTextState = new TextState { FontSize = 10 };

            // ----- Row 3 (regular) -----
            Row regularRow = table.Rows.Add();
            // No fixed height; the row will size to its content
            Cell regularCell1 = regularRow.Cells.Add("Regular Row Cell 1");
            Cell regularCell2 = regularRow.Cells.Add("Regular Row Cell 2");

            // Save the document.
            // Guard against missing GDI+ on non‑Windows platforms.
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF saving was skipped.");
            }
        }
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