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

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 24 pages.
            if (doc.Pages.Count < 24)
            {
                Console.Error.WriteLine("Document does not contain page 24.");
                return;
            }

            // Get page 24 (Aspose.Pdf uses 1‑based indexing).
            Page page24 = doc.Pages[24];

            // Create a TableAbsorber and visit only page 24.
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page24);

            // Copy the extracted tables to an array for further processing if needed.
            var tablesArray = absorber.TableList.Cast<AbsorbedTable>().ToArray();

            // Remove each extracted table from the page.
            foreach (var table in tablesArray)
            {
                absorber.Remove(table);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
