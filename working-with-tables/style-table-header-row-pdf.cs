using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Disable the native Aspose.Pdf helper executable on platforms where it is not available.
        // This avoids the runtime error "cannot find the file AsposePdfApi_*.exe".
        Environment.SetEnvironmentVariable("Aspose.Pdf.DisableNative", "true");

        const string inputPath = "input.pdf";
        const string outputPath = "styled_table.pdf";

        // Ensure the Aspose.Pdf assembly can be loaded – if it cannot, provide a clear message.
        try
        {
            // Force a type reference to trigger assembly load early.
            var _ = typeof(Document);
        }
        catch (FileNotFoundException ex)
        {
            Console.Error.WriteLine($"Aspose.Pdf assembly not found: {ex.Message}");
            return;
        }

        // Verify the input PDF exists; if not, create a minimal placeholder on Windows only.
        if (!File.Exists(inputPath))
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Create a simple one‑page PDF as a placeholder.
                var placeholder = new Document();
                placeholder.Pages.Add();
                placeholder.Save(inputPath);
                Console.WriteLine($"Placeholder PDF created at '{inputPath}'.");
            }
            else
            {
                Console.Error.WriteLine($"Input PDF '{inputPath}' not found and cannot create placeholder on non‑Windows platforms.");
                return;
            }
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure we have at least one page to place the table on
            Page page = doc.Pages[1]; // 1‑based indexing

            // Create a visual table
            Table table = new Table
            {
                // Optional: set column widths (in points)
                ColumnWidths = "100 150 100"
            };

            // ---------- Header Row ----------
            Row headerRow = table.Rows.Add();

            // Common text state for header cells (bold font)
            TextState headerTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Aspose.Pdf.Color.Black
            };

            // First header cell
            Cell headerCell1 = headerRow.Cells.Add("Product");
            headerCell1.BackgroundColor = Aspose.Pdf.Color.LightGray; // Distinct background
            headerCell1.DefaultCellTextState = headerTextState;        // Bold font

            // Second header cell
            Cell headerCell2 = headerRow.Cells.Add("Quantity");
            headerCell2.BackgroundColor = Aspose.Pdf.Color.LightGray;
            headerCell2.DefaultCellTextState = headerTextState;

            // Third header cell
            Cell headerCell3 = headerRow.Cells.Add("Price");
            headerCell3.BackgroundColor = Aspose.Pdf.Color.LightGray;
            headerCell3.DefaultCellTextState = headerTextState;

            // ---------- Data Rows (example) ----------
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Widget A");
            dataRow.Cells.Add("10");
            dataRow.Cells.Add("$5.00");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with styled header saved to '{outputPath}'.");
    }
}
