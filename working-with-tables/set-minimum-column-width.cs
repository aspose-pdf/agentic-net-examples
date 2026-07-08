using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string outputPath = "table_min_column_width.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set a minimum (default) column width in points
            // This width will be used for any column that does not have an explicit width
            Table table = new Table
            {
                // Example: three columns, each will be at least 80 points wide
                DefaultColumnWidth = "80"
            };

            // Optionally define explicit widths for columns (must be >= default width)
            // Here we set the first column to 100 points, second to 120 points, third will use the default
            table.ColumnWidths = "100 120";

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");
            // Apply a background color to distinguish the header
            header.BackgroundColor = Color.LightGray;

            // Add some data rows
            for (int i = 1; i <= 5; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i} - Col 1");
                row.Cells.Add($"Row {i} - Col 2");
                row.Cells.Add($"Row {i} - Col 3");
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}