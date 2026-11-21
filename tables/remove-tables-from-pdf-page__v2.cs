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

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 11 pages (Aspose.Pdf uses 1‑based indexing).
            if (doc.Pages.Count < 11)
            {
                Console.Error.WriteLine("The document does not contain page 11.");
                return;
            }

            // Create a TableAbsorber and visit page 11.
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[11]);

            // Copy the detected tables to a temporary list before removal to avoid
            // modifying the collection while iterating.
            var tables = new System.Collections.Generic.List<AbsorbedTable>(absorber.TableList);
            foreach (AbsorbedTable table in tables)
            {
                // Remove the table from the page via the absorber.
                absorber.Remove(table);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
