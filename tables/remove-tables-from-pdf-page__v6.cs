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
            // Verify that page 34 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 34)
            {
                Console.Error.WriteLine("The document contains fewer than 34 pages.");
                return;
            }

            // Create a TableAbsorber and visit page 34
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[34]);

            // Copy the extracted tables to an array for further processing
            var tablesArray = absorber.TableList
                                      .Cast<AbsorbedTable>()
                                      .ToArray();

            // Remove each extracted table from the page using the absorber's Remove method
            foreach (var table in tablesArray)
            {
                absorber.Remove(table);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
