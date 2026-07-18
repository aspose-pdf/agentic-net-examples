using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_repeating_header.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page; the table will automatically flow to additional pages
            Page page = doc.Pages.Add();

            // Create a table and specify that the first row should repeat on each page
            Table table = new Table
            {
                RepeatingRowsCount = 1,               // repeat the first row (header)
                ColumnWidths = "100 200 150"           // optional column width definition
            };

            // ----- Header row (will be repeated) -----
            Row header = table.Rows.Add();
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Description");

            // Apply a simple style to header cells
            foreach (Cell cell in header.Cells)
            {
                cell.DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    FontStyle = FontStyles.Bold,
                    ForegroundColor = Color.Black
                };
                cell.BackgroundColor = Color.LightGray;
            }

            // ----- Data rows (enough to span multiple pages) -----
            for (int i = 1; i <= 100; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(i.ToString());
                row.Cells.Add($"Item {i}");
                row.Cells.Add($"Description for item {i}");
            }

            // Add the table to the page; Aspose.Pdf will handle pagination automatically
            page.Paragraphs.Add(table);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}