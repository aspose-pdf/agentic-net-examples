using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_tables.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Create a TableAbsorber to locate tables on the current page
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from the page
                absorber.Visit(page);

                // Make a copy of the TableList because Remove modifies the collection
                var tables = absorber.TableList.ToList();

                // Remove each detected table from the page
                foreach (var table in tables)
                {
                    absorber.Remove(table);
                }
            }

            // Save the modified document (PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tables removed. Output saved to '{outputPath}'.");
    }
}