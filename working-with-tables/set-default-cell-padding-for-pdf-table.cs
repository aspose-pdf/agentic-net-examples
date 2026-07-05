using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_padding.pdf";

        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100"
            };

            // Define default cell padding for the entire table
            MarginInfo padding = new MarginInfo();
            padding.Top = 5;      // 5 points top padding
            padding.Bottom = 5;   // 5 points bottom padding
            padding.Left = 5;     // 5 points left padding
            padding.Right = 5;    // 5 points right padding
            table.DefaultCellPadding = padding;

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a few data rows
            for (int i = 1; i <= 3; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i} Col 1");
                row.Cells.Add($"Row {i} Col 2");
                row.Cells.Add($"Row {i} Col 3");
            }

            // Place the table on the page
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}