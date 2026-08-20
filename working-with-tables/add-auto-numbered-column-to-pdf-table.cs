using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_numbers.pdf";

        // Document lifecycle must be wrapped in a using block
        using (Document doc = new Document())
        {
            // Add a page to host the table
            Page page = doc.Pages.Add();

            // Create a table with three columns (first column will hold numbers)
            Table table = new Table
            {
                // Column widths: 50 units for the number column, 150 for each data column
                ColumnWidths = "50 150 150",
                // Optional styling for cells – use BorderInfo constructor (no Width property)
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Header row (no number in the first cell)
            Row header = table.Rows.Add();
            header.Cells.Add("");               // placeholder for number column
            header.Cells.Add("Name");
            header.Cells.Add("Value");
            // Header text style
            header.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                FontStyle = FontStyles.Bold
            };

            // Sample data rows (placeholders for numbers)
            string[] names  = { "Alpha", "Beta", "Gamma" };
            string[] values = { "10", "20", "30" };

            for (int i = 0; i < names.Length; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add("");               // placeholder for auto‑number
                row.Cells.Add(names[i]);
                row.Cells.Add(values[i]);
            }

            // Build an array with sequential numbers (skip header row)
            object[] numbers = new object[table.Rows.Count];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = (i == 0) ? "" : i.ToString(); // header stays empty, rows start at 1
            }

            // Import the numbers into the first column of the table
            // firstFilledRow = 0, firstFilledColumn = 0, isLeftColumnsFilled = false
            table.ImportArray(numbers, 0, 0, false);

            // Add the table to the page and save the document
            page.Paragraphs.Add(table);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with auto‑numbered column saved to '{outputPath}'.");
    }
}
