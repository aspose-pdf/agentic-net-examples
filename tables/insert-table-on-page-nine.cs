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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 9 pages
            if (doc.Pages.Count < 9)
            {
                Console.Error.WriteLine("The document does not contain page 9.");
                return;
            }

            // Create a new Table object
            Table table = new Table();

            // Define column widths (space‑separated string). Adjust as needed.
            table.ColumnWidths = "150 150 150";

            // Add a row to the table
            Row row = table.Rows.Add();

            // Populate cells in the row
            row.Cells.Add("Column 1");
            row.Cells.Add("Column 2");
            row.Cells.Add("Column 3");

            // Access page 9 (Aspose.Pdf uses 1‑based indexing)
            Page pageNine = doc.Pages[9];

            // Remove existing content on the page (optional, depending on requirements)
            pageNine.Paragraphs.Clear();

            // Add the newly created table to page 9
            pageNine.Paragraphs.Add(table);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted on page 9 and saved to '{outputPath}'.");
    }
}
