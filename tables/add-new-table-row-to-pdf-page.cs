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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 13 pages (1‑based indexing)
            if (doc.Pages.Count < 13)
            {
                Console.Error.WriteLine("The document does not contain page 13.");
                return;
            }

            // Get page 13
            Page page13 = doc.Pages[13];

            // Use TableAbsorber to locate existing tables on page 13 (optional – we just need a table later)
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page13);

            // Create a new table at a fixed position (left = 100, top = 500 points from the top of the page)
            Table newTable = new Table
            {
                // Position the table using Left and Top (BaseParagraph properties)
                Left = 100,
                Top = 500,
                // Optional: set a default column width string if you want two equal columns
                ColumnWidths = "200 200"
            };

            // Add a single row with two cells
            Row row = newTable.Rows.Add();
            row.Cells.Add("New Cell 1");
            row.Cells.Add("New Cell 2");

            // Add the new table to the page
            page13.Paragraphs.Add(newTable);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
