using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "TableWithRepeatingHeader.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to host the table
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Position the table (left, top) and set column widths as needed
                ColumnWidths = "100 150 200",
                // The first 2 rows will be repeated on each page when the table breaks
                RepeatingRowsCount = 2
            };

            // ----- Header rows (will be repeated) -----
            // Row 1 – column titles
            Row headerRow1 = table.Rows.Add();
            headerRow1.Cells.Add("Product");
            headerRow1.Cells.Add("Category");
            headerRow1.Cells.Add("Price");

            // Row 2 – sub‑header (example)
            Row headerRow2 = table.Rows.Add();
            headerRow2.Cells.Add("Name");
            headerRow2.Cells.Add("Type");
            headerRow2.Cells.Add("Amount");

            // Apply a simple style to header rows (optional)
            foreach (Cell cell in headerRow1.Cells)
                cell.DefaultCellTextState = new TextState { FontSize = 12, FontStyle = FontStyles.Bold, ForegroundColor = Color.Blue };
            foreach (Cell cell in headerRow2.Cells)
                cell.DefaultCellTextState = new TextState { FontSize = 11, FontStyle = FontStyles.Bold, ForegroundColor = Color.DarkGray };

            // ----- Data rows (will flow across pages) -----
            // For demonstration, generate many rows to force a page break
            for (int i = 1; i <= 100; i++)
            {
                Row dataRow = table.Rows.Add();
                dataRow.Cells.Add($"Item {i}");
                dataRow.Cells.Add(i % 2 == 0 ? "Even" : "Odd");
                dataRow.Cells.Add($"${i * 10}");
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'. Header rows will repeat on each page.");
    }
}