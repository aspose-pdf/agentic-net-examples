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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 17 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 17)
            {
                Console.Error.WriteLine("The document has fewer than 17 pages.");
                return;
            }

            // -------------------------------------------------
            // Create a new table
            // -------------------------------------------------
            Table table = new Table
            {
                // Define column widths (values are in points; space‑separated string)
                ColumnWidths = "100 150 200"
            };

            // Add a single row
            Row row = table.Rows.Add();

            // Populate cells with text
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");
            row.Cells.Add("Cell 3");

            // -------------------------------------------------
            // Replace any existing tables on page 17
            // -------------------------------------------------
            Page page17 = doc.Pages[17];

            // Use TableAbsorber to locate and remove existing tables (core API, no Delete method)
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page17);
            // Remove each absorbed table from the page
            foreach (var absorbedTable in absorber.TableList.ToArray())
            {
                absorber.Remove(absorbedTable);
            }

            // Insert the newly created table
            page17.Paragraphs.Add(table);

            // -------------------------------------------------
            // Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
