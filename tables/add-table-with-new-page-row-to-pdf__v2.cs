using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table with three columns (adjust widths as needed)
            Table table = new Table
            {
                ColumnWidths = "150 150 150"
            };

            // First row
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Row 1, Col 1");
            row1.Cells.Add("Row 1, Col 2");
            row1.Cells.Add("Row 1, Col 3");

            // Second row
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Row 2, Col 1");
            row2.Cells.Add("Row 2, Col 2");
            row2.Cells.Add("Row 2, Col 3");

            // Third row – start on a new page
            Row row3 = table.Rows.Add();
            row3.IsInNewPage = true;
            row3.Cells.Add("Row 3, Col 1");
            row3.Cells.Add("Row 3, Col 2");
            row3.Cells.Add("Row 3, Col 3");

            // Add the table to the page's content
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}