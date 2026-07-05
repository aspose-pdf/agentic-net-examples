using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "table_fixed_width.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a table with a single column of fixed width 500 points
            Table table = new Table
            {
                // ColumnWidths expects a string; for a single column set it to "500"
                ColumnWidths = "500"
            };

            // Optionally set a default column width (useful if more columns are added later)
            table.DefaultColumnWidth = "500";

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell with some text
            Cell cell = row.Cells.Add();
            // Use a TextFragment to add styled text to the cell
            TextFragment tf = new TextFragment("This cell has a fixed width of 500 points.");
            cell.Paragraphs.Add(tf);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with fixed-width table saved to '{outputPath}'.");
    }
}