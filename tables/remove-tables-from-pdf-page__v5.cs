using System;
using System.IO;
using System.Linq;
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
            // Verify that the document contains at least 29 pages (1‑based indexing)
            if (doc.Pages.Count < 29)
            {
                Console.Error.WriteLine("Document has fewer than 29 pages.");
                return;
            }

            // Access page 29
            Page page = doc.Pages[29];

            // Extract tables from the page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);

            // Copy the extracted tables (AbsorbedTable objects) to an array for further processing
            var tablesArray = absorber.TableList.Cast<AbsorbedTable>().ToArray();

            // Remove each extracted table from the page using the absorber
            foreach (var absorbedTable in tablesArray)
            {
                absorber.Remove(absorbedTable);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
