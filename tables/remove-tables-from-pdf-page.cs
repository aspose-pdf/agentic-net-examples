using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for TableAbsorber

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
            // Ensure the document has at least four pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document does not contain a fourth page.");
                return;
            }

            // Get page four
            Page pageFour = doc.Pages[4];

            // Create a TableAbsorber and visit page four to populate TableList
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(pageFour);

            // Copy the detected tables to an array for further processing or inspection
            var tablesArray = absorber.TableList.ToArray();

            // (Optional) Example: output the number of tables found
            Console.WriteLine($"Found {tablesArray.Length} table(s) on page 4.");

            // Remove each detected table from the page
            foreach (var tbl in tablesArray)
            {
                absorber.Remove(tbl);
            }

            // Save the modified PDF (default PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
