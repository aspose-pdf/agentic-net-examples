using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int insertColumnIndex = 1; // zero‑based index where the new column will be inserted

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // Create a simple table with 3 columns and 3 rows (for demonstration)
            Table table = new Table
            {
                // Initial column widths (three columns)
                ColumnWidths = "100 100 100",
                // Simple border for visibility
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Populate the table with sample data
            for (int r = 0; r < 3; r++)
            {
                Row row = table.Rows.Add(); // Add a new row
                for (int c = 0; c < 3; c++)
                {
                    // Add a cell with text "R{row}C{col}"
                    Cell cell = row.Cells.Add($"R{r + 1}C{c + 1}");
                    // Optional: set cell border (use the correct property name)
                    cell.Border = new BorderInfo(BorderSide.All, 0.5f);
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Insert a new column at the specified index by adding a Cell to each Row
            foreach (Row row in table.Rows)
            {
                // Create a new empty cell
                Cell newCell = new Cell();

                // Optionally add placeholder text to the new cell
                newCell.Paragraphs.Add(new TextFragment("New"));

                // Insert the cell at the desired column index
                row.Cells.Insert(insertColumnIndex, newCell);
            }

            // Adjust column widths to accommodate the new column (equal widths for simplicity)
            int columnCount = table.Rows[0].Cells.Count; // assume at least one row exists
            string[] widths = new string[columnCount];
            for (int i = 0; i < columnCount; i++) widths[i] = "100";
            table.ColumnWidths = string.Join(" ", widths);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Column inserted and saved to '{outputPath}'.");
    }
}
