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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 23 pages
            if (doc.Pages.Count < 23)
            {
                Console.Error.WriteLine("The document has fewer than 23 pages.");
                return;
            }

            // Create a new Table
            Table table = new Table
            {
                // Set column widths (example: two columns, 150 and 250 points)
                ColumnWidths = "150 250"
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add cells to the row
            row.Cells.Add("First cell content");
            row.Cells.Add("Second cell content");

            // Access page 23 (Aspose.Pdf uses 1‑based indexing)
            Page page23 = doc.Pages[23];

            // Remove any existing tables on page 23 using TableAbsorber
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page23);
            // Copy the list because Remove modifies the collection while iterating
            var tablesToRemove = new System.Collections.Generic.List<AbsorbedTable>(absorber.TableList);
            foreach (var absorbedTable in tablesToRemove)
            {
                absorber.Remove(absorbedTable);
            }

            // Add the newly created table to page 23
            page23.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
