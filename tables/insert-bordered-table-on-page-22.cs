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
            // Ensure the document has at least 22 pages
            if (doc.Pages.Count < 22)
            {
                Console.Error.WriteLine("The document does not contain page 22.");
                return;
            }

            // Create a new Table
            Table table = new Table();

            // Set a default cell border (all sides, 0.5 point width, black color)
            table.DefaultCellBorder = new BorderInfo(
                BorderSide.All,
                0.5f,
                Aspose.Pdf.Color.Black);

            // Optionally define column widths (three equal columns)
            table.ColumnWidths = "100 100 100";

            // Add multiple rows with sample data
            for (int r = 1; r <= 5; r++) // create 5 rows as an example
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {r} - Col 1");
                row.Cells.Add($"Row {r} - Col 2");
                row.Cells.Add($"Row {r} - Col 3");
            }

            // Access page 22 (Aspose.Pdf uses 1‑based indexing)
            Page page22 = doc.Pages[22];

            // Remove any existing tables on the page using TableAbsorber
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page22);
            // Copy the list because removing modifies the collection while iterating
            var tablesToRemove = new System.Collections.Generic.List<AbsorbedTable>(absorber.TableList);
            foreach (var absorbed in tablesToRemove)
            {
                absorber.Remove(absorbed);
            }

            // Add the newly created table to page 22
            page22.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
