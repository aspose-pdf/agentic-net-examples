using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "wide_table_landscape.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Set the page size to landscape (A4 landscape: width > height)
            page.PageInfo.Width = 842;   // points (approximately 297 mm)
            page.PageInfo.Height = 595;  // points (approximately 210 mm)

            // Create a table with many columns to demonstrate a wide layout
            Table table = new Table
            {
                // Set the table width to 100% of the page width
                ColumnWidths = "10% 10% 10% 10% 10% 10% 10% 10% 10% 10%",
                // Optional: add a border for visual clarity
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add a header row
            Row header = table.Rows.Add();
            for (int i = 1; i <= 10; i++)
            {
                Cell headerCell = header.Cells.Add($"Header {i}");
                headerCell.BackgroundColor = Color.LightGray;
                // Use DefaultCellTextState to format cell text
                headerCell.DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    FontStyle = FontStyles.Bold
                };
            }

            // Add a few data rows
            for (int r = 1; r <= 5; r++)
            {
                Row dataRow = table.Rows.Add();
                for (int c = 1; c <= 10; c++)
                {
                    Cell dataCell = dataRow.Cells.Add($"R{r}C{c}");
                    // Optional: set a default text state for data cells
                    dataCell.DefaultCellTextState = new TextState { FontSize = 10 };
                }
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the document (PDF format) inside the using block
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with landscape orientation saved to '{outputPath}'.");
    }
}
