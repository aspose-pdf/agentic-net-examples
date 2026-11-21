using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a table and add it to the first page
            Table table = new Table
            {
                // Define column widths (optional)
                ColumnWidths = "150 150"
            };
            doc.Pages[1].Paragraphs.Add(table); // Pages are 1‑based

            // Add a new row to the table
            Row row = table.Rows.Add();

            // Set a fixed height for this row (value is in points)
            row.FixedRowHeight = 40; // 40 points ≈ 0.56 inch

            // Add cells to the row
            Cell cell1 = row.Cells.Add("Cell 1");
            Cell cell2 = row.Cells.Add("Cell 2");

            // Optional: set cell text formatting
            cell1.DefaultCellTextState.FontSize = 12;
            cell2.DefaultCellTextState.FontSize = 12;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom row height to '{outputPath}'.");
    }
}
