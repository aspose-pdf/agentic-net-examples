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
            // Ensure the document has at least one page
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("Document has no pages.");
                return;
            }

            // Get the first page
            Page page = doc.Pages[1];

            // Remove existing content (including any existing tables) from the page
            page.Paragraphs.Clear();

            // Create a new three‑column table
            Table table = new Table
            {
                // Define column widths (in points). Adjust as needed.
                ColumnWidths = "120 150 180"
            };

            // Add a header row (optional)
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Make the first row repeat as a header on each new page
            table.RepeatingRowsCount = 1;

            // Add sample data rows
            for (int i = 1; i <= 5; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i} - Col 1");
                row.Cells.Add($"Row {i} - Col 2");
                row.Cells.Add($"Row {i} - Col 3");
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
