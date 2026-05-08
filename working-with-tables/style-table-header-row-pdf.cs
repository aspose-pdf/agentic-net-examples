using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "styled_table.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table
            {
                // Define three column widths (adjust as needed)
                ColumnWidths = "100 150 150"
            };
            page.Paragraphs.Add(table);

            // ----- Header row with distinct style -----
            Row headerRow = new Row
            {
                // Background color for the header row
                BackgroundColor = Aspose.Pdf.Color.LightGray
            };

            // Set default text state for header cells (bold font)
            headerRow.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Aspose.Pdf.Color.Black
            };

            // Add header cells
            headerRow.Cells.Add("Product");
            headerRow.Cells.Add("Quantity");
            headerRow.Cells.Add("Price");

            // Append the styled header row to the table
            table.Rows.Add(headerRow);

            // ----- Data rows (regular style) -----
            for (int i = 0; i < 5; i++)
            {
                Row dataRow = new Row();
                dataRow.Cells.Add($"Item {i + 1}");
                dataRow.Cells.Add((10 + i).ToString());
                dataRow.Cells.Add($"${(20 + i * 5):0.00}");
                table.Rows.Add(dataRow);
            }

            // Save the PDF; handle missing GDI+ on non‑Windows platforms
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
                    Console.WriteLine("GDI+ (libgdiplus) not available; PDF could not be saved on this platform.");
                }
            }
        }
    }

    // Helper method to detect a nested DllNotFoundException
    static bool ContainsDllNotFound(Exception ex)
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