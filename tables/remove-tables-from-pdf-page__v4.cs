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

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                if (doc.Pages.Count < 19)
                {
                    Console.Error.WriteLine("Document has fewer than 19 pages.");
                    return;
                }

                // Extract tables from page 19 using TableAbsorber (core API)
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(doc.Pages[19]);

                // Copy detected tables to an array of AbsorbedTable
                AbsorbedTable[] tablesArray = absorber.TableList?.ToArray() ?? Array.Empty<AbsorbedTable>();
                Console.WriteLine($"Found {tablesArray.Length} table(s) on page 19.");

                // Remove each detected table from the document
                foreach (var tbl in tablesArray)
                {
                    absorber.Remove(tbl);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
