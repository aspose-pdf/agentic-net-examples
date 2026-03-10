using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "TableWithCells.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Define column widths (three columns, each 100 points wide)
                ColumnWidths = "100 100 100",
                // Set default cell border using the modern BorderInfo class
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };
            // Optional: you can set the table to auto‑fit columns; if the enum is unavailable, simply omit this line.
            // table.ColumnAdjustment = ColumnAdjustment.AutoFit;
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);
            table.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Color.Black
            };

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Define how many rows we want
            const int rowCount = 5;

            // Loop to create rows and populate each with cells
            for (int r = 0; r < rowCount; r++)
            {
                // Add a new row to the table
                Row row = table.Rows.Add();

                // Add three regular cells
                row.Cells.Add($"Row {r + 1} - Col 1");
                row.Cells.Add($"Row {r + 1} - Col 2");
                row.Cells.Add($"Row {r + 1} - Col 3");

                // OPTIONAL: demonstrate inserting a cell at a specific index
                // Insert a new cell at position 1 (second column) with custom text state
                Cell insertedCell = new Cell
                {
                    Paragraphs = { new TextFragment($"Inserted at row {r + 1}") },
                    DefaultCellTextState = new TextState
                    {
                        Font = FontRepository.FindFont("TimesNewRoman"),
                        FontSize = 10,
                        ForegroundColor = Color.Blue
                    }
                };
                // Insert the cell at index 1 (shifts existing cells to the right)
                row.Cells.Insert(1, insertedCell);
            }

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with populated table saved to '{outputPath}'.");
    }
}
