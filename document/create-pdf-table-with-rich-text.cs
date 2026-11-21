using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "rich_text_table.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (Pages collection is 1‑based)
            Page page = doc.Pages.Add();

            // Create a table using the core Table API (no TaggedContent, no Facades)
            Table table = new Table
            {
                ColumnWidths = "200 200" // column widths in points
            };

            // ---------- Header Row ----------
            Row headerRow = table.Rows.Add();

            // First header cell with bold text
            Cell headerCell1 = headerRow.Cells.Add();
            TextFragment tfHeader1 = new TextFragment("Header 1")
            {
                TextState = { FontStyle = FontStyles.Bold, FontSize = 12 }
            };
            headerCell1.Paragraphs.Add(tfHeader1);

            // Second header cell with italic text
            Cell headerCell2 = headerRow.Cells.Add();
            TextFragment tfHeader2 = new TextFragment("Header 2")
            {
                TextState = { FontStyle = FontStyles.Italic, FontSize = 12 }
            };
            headerCell2.Paragraphs.Add(tfHeader2);

            // ---------- Data Row ----------
            Row dataRow = table.Rows.Add();

            // First data cell with mixed formatting (bold + red)
            Cell dataCell1 = dataRow.Cells.Add();
            TextFragment tfData1 = new TextFragment("Bold Red Text")
            {
                TextState = { FontStyle = FontStyles.Bold, FontSize = 10, ForegroundColor = Color.FromRgb(1, 0, 0) }
            };
            dataCell1.Paragraphs.Add(tfData1);

            // Second data cell with underline and blue color
            Cell dataCell2 = dataRow.Cells.Add();
            TextFragment tfData2 = new TextFragment("Underlined Blue")
            {
                TextState = { FontSize = 10, ForegroundColor = Color.FromRgb(0, 0, 1), Underline = true }
            };
            dataCell2.Paragraphs.Add(tfData2);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rich‑text table saved to '{outputPath}'.");
    }
}
