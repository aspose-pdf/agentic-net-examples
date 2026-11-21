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
            // Ensure the document has at least 15 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 15)
            {
                Console.Error.WriteLine("The document does not contain page 15.");
                return;
            }

            // Get page 15
            Page page = doc.Pages[15];

            // Create a new table
            Table newTable = new Table
            {
                ColumnWidths = "100 200 150"
            };

            // Add a row and cells
            Row row = newTable.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");
            row.Cells.Add("Cell 3");

            // Try to locate an existing table on the page and replace it.
            // TableAbsorber works with the core Table API and does not require any Facade classes.
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);

            if (absorber.TableList.Count > 0)
            {
                // Replace the first found table with the new one, preserving its position.
                absorber.Replace(page, absorber.TableList[0], newTable);
            }
            else
            {
                // No existing table – simply add the new table to the page.
                page.Paragraphs.Add(newTable);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
