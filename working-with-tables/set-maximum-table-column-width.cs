using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_table.pdf";
        const double maxColumnWidth = 200; // points

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to place the table on
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Desired column widths (points) before applying the max constraint
            double col1Width = 250; // exceeds max
            double col2Width = 150; // within max

            // Enforce maximum column width
            col1Width = Math.Min(col1Width, maxColumnWidth);
            col2Width = Math.Min(col2Width, maxColumnWidth);

            // Create a table and set the column widths
            Table table = new Table
            {
                // ColumnWidths expects a string with widths separated by spaces
                ColumnWidths = $"{col1Width} {col2Width}",
                // Optional: add a thin border to all cells for visual clarity
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // ----- Header -----
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");

            // ----- Body -----
            Row bodyRow = table.Rows.Add();
            bodyRow.Cells.Add("Cell with long content that would normally expand the column beyond the limit.");
            bodyRow.Cells.Add("Short cell");

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with limited column width to '{outputPath}'.");
    }
}
