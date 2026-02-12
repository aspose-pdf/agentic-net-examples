using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        try
        {
            // Verify the source PDF exists
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
                return;
            }

            // Load the existing PDF document
            Document pdfDoc = new Document(inputPath);

            // ------------------------------------------------------------
            // 1. Create a visual table and add it to the first page
            // ------------------------------------------------------------
            Page page = pdfDoc.Pages[1];

            // Create a table with three columns. Use BorderInfo (cross‑platform) instead of the obsolete Border/BorderStyle classes.
            Table table = new Table
            {
                ColumnWidths = "120 200 120",
                DefaultCellBorder = new BorderInfo(BorderSide.All, 1) // solid 1‑pt border on all sides
            };

            // Header row
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add().Paragraphs.Add(new TextFragment("Product"));
            headerRow.Cells.Add().Paragraphs.Add(new TextFragment("Description"));
            headerRow.Cells.Add().Paragraphs.Add(new TextFragment("Price"));

            // Sample data rows
            Row row1 = table.Rows.Add();
            row1.Cells.Add().Paragraphs.Add(new TextFragment("Apple"));
            row1.Cells.Add().Paragraphs.Add(new TextFragment("Fresh red apple"));
            row1.Cells.Add().Paragraphs.Add(new TextFragment("$1.20"));

            Row row2 = table.Rows.Add();
            row2.Cells.Add().Paragraphs.Add(new TextFragment("Banana"));
            row2.Cells.Add().Paragraphs.Add(new TextFragment("Ripe yellow banana"));
            row2.Cells.Add().Paragraphs.Add(new TextFragment("$0.80"));

            // Position the table on the page
            table.Margin = new MarginInfo { Top = 50, Left = 50 };
            page.Paragraphs.Add(table);

            // ------------------------------------------------------------
            // 2. (Optional) Tagged PDF support – omitted for cross‑platform compatibility
            // ------------------------------------------------------------
            // The original example used TaggedContent, TableElement, TableTHeadElement, etc.
            // Those classes are not available in the current Aspose.Pdf version used for this
            // build, and they are also not required for the visual table demonstration.
            // If tagged‑PDF functionality is needed, upgrade to a version that includes the
            // Aspose.Pdf.Tagged namespace and use the appropriate API.

            // ------------------------------------------------------------
            // 3. Save the modified PDF
            // ------------------------------------------------------------
            pdfDoc.Save(outputPath);
            Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
