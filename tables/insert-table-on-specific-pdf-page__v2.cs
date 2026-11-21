using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 24 pages
            if (doc.Pages.Count < 24)
            {
                Console.Error.WriteLine("The document has fewer than 24 pages.");
                return;
            }

            // Get page 24 (1‑based indexing)
            Page page24 = doc.Pages[24];

            // Remove existing content (paragraphs) on the page
            page24.Paragraphs.Clear();

            // Create a new table
            Table table = new Table
            {
                // Define column widths (e.g., two columns: 150 and 250 points)
                ColumnWidths = "150 250"
            };

            // Create a new row
            Row row = new Row();

            // First cell with some text
            Cell cell1 = new Cell
            {
                Paragraphs = { new TextFragment("Cell 1 content") }
            };
            row.Cells.Add(cell1);

            // Second cell with some text
            Cell cell2 = new Cell
            {
                Paragraphs = { new TextFragment("Cell 2 content") }
            };
            row.Cells.Add(cell2);

            // Add the row to the table
            table.Rows.Add(row);

            // Add the table to the page's paragraphs (replaces previous content)
            page24.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}