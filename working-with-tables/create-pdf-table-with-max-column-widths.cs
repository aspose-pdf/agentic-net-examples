using System;
using System.IO;
using System.Runtime.InteropServices; // Added for OS check
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with 3 columns and set maximum column widths (in points)
            Table table = new Table
            {
                // Widths are specified as a space‑separated string of point values.
                // These values act as the maximum width each column can occupy.
                ColumnWidths = "120 150 180", // 120pt, 150pt, 180pt
                // Default width for any additional columns that might be added later.
                DefaultColumnWidth = "100"
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");
            // Apply a simple style to the header
            foreach (Cell cell in header.Cells)
            {
                cell.BackgroundColor = Color.LightGray;
                cell.DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    FontStyle = FontStyles.Bold,
                    ForegroundColor = Color.Black
                };
            }

            // Add a few data rows
            for (int i = 1; i <= 5; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i} - Col 1");
                row.Cells.Add($"Row {i} - Col 2 with a longer text that should wrap if it exceeds the column width");
                row.Cells.Add($"Row {i} - Col 3");

                // Enable word wrap for cells that may exceed the column width
                foreach (Cell cell in row.Cells)
                {
                    cell.DefaultCellTextState = new TextState
                    {
                        Font = FontRepository.FindFont("Helvetica"),
                        FontSize = 10,
                        ForegroundColor = Color.Black
                    };
                    // Correct property for enabling word wrap
                    cell.IsWordWrapped = true;
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF – guard the call on macOS/Linux where libgdiplus may be missing
            string outputPath = "TableWithLimitedColumnWidths.pdf";
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine("PDF creation routine finished.");
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
