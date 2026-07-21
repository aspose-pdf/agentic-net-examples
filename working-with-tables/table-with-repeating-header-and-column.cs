using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "table_with_repeating_column.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and configure it
            Table table = new Table
            {
                // Define column widths (example: three columns)
                ColumnWidths = "100 150 200",
                // Repeat the first column on each new page when the table splits
                RepeatingColumnsCount = 1,
                // Repeat the first row (header) on each new page
                RepeatingRowsCount = 1,
                // Optional: set table border for visibility
                Border = new BorderInfo(BorderSide.All, 1, Color.Black)
            };

            // -------------------------
            // Create the header row (will be repeated automatically)
            // -------------------------
            Row headerRow = table.Rows.Add();
            Cell cell1 = headerRow.Cells.Add("ID");
            cell1.BackgroundColor = Color.LightGray;
            Cell cell2 = headerRow.Cells.Add("Name");
            cell2.BackgroundColor = Color.LightGray;
            Cell cell3 = headerRow.Cells.Add("Description");
            cell3.BackgroundColor = Color.LightGray;

            // -------------------------
            // Add data rows
            // -------------------------
            for (int i = 1; i <= 50; i++) // enough rows to force a page break
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"#{i}");
                row.Cells.Add($"Item {i}");
                row.Cells.Add($"This is a description for item {i}.");
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
