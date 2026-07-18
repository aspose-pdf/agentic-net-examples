using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_numbers.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and configure basic appearance
            Table table = new Table
            {
                // Define column widths (first column for numbers)
                ColumnWidths = "50 150 150",
                // Optional: add borders and padding for readability
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Helper local function to create a cell containing plain text
            Cell CreateTextCell(string text)
            {
                Cell cell = new Cell();
                cell.Paragraphs.Add(new TextFragment(text));
                return cell;
            }

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add(CreateTextCell("No"));
            header.Cells.Add(CreateTextCell("Name"));
            header.Cells.Add(CreateTextCell("Value"));

            // Number of data rows to generate
            int dataRows = 10;

            // Add data rows with an auto‑numbered first column
            for (int i = 1; i <= dataRows; i++)
            {
                Row row = table.Rows.Add();

                // First cell: sequential number
                row.Cells.Add(CreateTextCell(i.ToString()));

                // Additional cells (example content)
                row.Cells.Add(CreateTextCell($"Item {i}"));
                row.Cells.Add(CreateTextCell($"Value {i * 100}"));
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
