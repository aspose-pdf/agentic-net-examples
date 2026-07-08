using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string outputPath = "table_fixed_row_height.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define a default row height that will be applied to every row we add
            const float defaultRowHeight = 30f;

            // Create a table and set column widths (3 columns in this example)
            Table table = new Table
            {
                // Optional: set column widths (3 columns in this example)
                ColumnWidths = "100 150 100"
            };

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Add header row
            Row header = table.Rows.Add();
            header.FixedRowHeight = defaultRowHeight; // apply the fixed height
            header.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 12,
                ForegroundColor = Color.White
            };
            header.BackgroundColor = Color.DarkBlue;
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Quantity");

            // Add data rows
            for (int i = 1; i <= 5; i++)
            {
                Row row = table.Rows.Add();
                row.FixedRowHeight = defaultRowHeight; // apply the fixed height
                row.Cells.Add(i.ToString());
                row.Cells.Add($"Item {i}");
                row.Cells.Add((i * 10).ToString());
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
