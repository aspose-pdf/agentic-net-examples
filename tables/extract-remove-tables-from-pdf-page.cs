using System;
using System.Collections.Generic;
using System.IO;
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
            // Ensure the document has at least 8 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 8)
            {
                Console.Error.WriteLine("The document does not contain page 8.");
                return;
            }

            // Create a TableAbsorber to extract tables from page 8
            TableAbsorber absorber = new TableAbsorber();

            // Visit page 8 (index 8) and collect tables
            absorber.Visit(doc.Pages[8]);

            // Copy the detected tables to an array for further processing or inspection
            AbsorbedTable[] tablesArray = absorber.TableList.ToArray();

            // (Optional) Example: output the number of tables found
            Console.WriteLine($"Tables found on page 8: {tablesArray.Length}");

            // Remove the extracted tables from the document – TableAbsorber.Remove takes a single AbsorbedTable
            foreach (AbsorbedTable tbl in tablesArray)
            {
                absorber.Remove(tbl);
            }

            // Save the modified PDF (default format is PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
