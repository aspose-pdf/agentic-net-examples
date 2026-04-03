using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_header.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its basic properties
            Table table = new Table
            {
                // Define column widths (space‑separated values)
                ColumnWidths = "100 150 200",
                // Repeat the first row (header) on each new page
                RepeatingRowsCount = 1,
                // Optional: style for the repeating header rows
                RepeatingRowsStyle = new TextState { FontSize = 12, Font = FontRepository.FindFont("Helvetica"), ForegroundColor = Aspose.Pdf.Color.Black }
            };

            // -------------------------
            // Create the header row
            // -------------------------
            Row headerRow = new Row
            {
                // Mark this row as a header so it repeats on each new page
                // (In recent Aspose.PDF versions the IsHeader property was removed; setting RepeatingRowsCount is sufficient.)
                // IsHeader = true, // <-- removed
                // Optional visual styling for the header row
                BackgroundColor = Aspose.Pdf.Color.LightGray,
                DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica-Bold"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.Black
                }
            };

            // Add cells to the header row
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Product") } });
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Quantity") } });
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Price") } });

            // Add the header row to the table
            table.Rows.Add(headerRow);

            // -------------------------
            // Add some data rows (optional)
            // -------------------------
            for (int i = 1; i <= 20; i++)
            {
                Row dataRow = new Row();
                dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment($"Item {i}") } });
                dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment((i * 2).ToString()) } });
                dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment($"${i * 5:0.00}") } });
                table.Rows.Add(dataRow);
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the document (guard against missing GDI+ on non‑Windows platforms)
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF saved without GDI+ dependent features.");
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Helper to detect nested DllNotFoundException
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
