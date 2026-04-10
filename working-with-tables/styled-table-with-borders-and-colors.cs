using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "TableStyled.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Position the table (left, top)
                Left = 50,
                Top = 700,
                // Define column widths (space‑separated string)
                ColumnWidths = "200 200"
            };

            // Apply a uniform border to all cells
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 1);
            // Set default cell padding (optional)
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);

            // ---------- Header Row ----------
            Row headerRow = new Row();
            // Header cell 1
            Cell headerCell1 = new Cell
            {
                BackgroundColor = Color.LightGray,
                DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 14,
                    ForegroundColor = Color.Black,
                    FontStyle = FontStyles.Bold
                }
            };
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            // Header cell 2
            Cell headerCell2 = new Cell
            {
                BackgroundColor = Color.LightGray,
                DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 14,
                    ForegroundColor = Color.Black,
                    FontStyle = FontStyles.Bold
                }
            };
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));
            // Add cells to the header row
            headerRow.Cells.Add(headerCell1);
            headerRow.Cells.Add(headerCell2);
            // Add header row to the table
            table.Rows.Add(headerRow);

            // ---------- Data Row ----------
            Row dataRow = new Row();
            // Data cell 1
            Cell dataCell1 = new Cell
            {
                BackgroundColor = Color.FromRgb(0.9, 0.9, 0.9), // light gray
                DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    ForegroundColor = Color.DarkBlue
                }
            };
            dataCell1.Paragraphs.Add(new TextFragment("Row 1, Col 1"));
            // Data cell 2
            Cell dataCell2 = new Cell
            {
                BackgroundColor = Color.FromRgb(0.9, 0.9, 0.9),
                DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    ForegroundColor = Color.DarkBlue
                }
            };
            dataCell2.Paragraphs.Add(new TextFragment("Row 1, Col 2"));
            // Add cells to the data row
            dataRow.Cells.Add(dataCell1);
            dataRow.Cells.Add(dataCell2);
            // Add data row to the table
            table.Rows.Add(dataRow);

            // Add the configured table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing libgdiplus on non‑Windows platforms
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
                    Console.WriteLine($"PDF saved to '{outputPath}' (non‑Windows platform)." );
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved using Aspose.Pdf's default renderer.");
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
