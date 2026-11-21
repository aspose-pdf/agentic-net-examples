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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 6 pages
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The document has fewer than 6 pages.");
                return;
            }

            // Create a new Table object
            Table table = new Table();

            // Define column widths (in points). Adjust as needed.
            // Example: three columns with widths 100, 200, and 150 points.
            table.ColumnWidths = "100 200 150";

            // Add a row to the table
            Row row = table.Rows.Add();

            // Populate cells in the row
            row.Cells.Add("Column 1 Text");
            row.Cells.Add("Column 2 Text");
            row.Cells.Add("Column 3 Text");

            // Access page six (1‑based indexing)
            Page pageSix = doc.Pages[6];

            // Remove existing content on page six (optional: keep other elements)
            pageSix.Paragraphs.Clear();

            // Add the newly created table to page six
            pageSix.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}