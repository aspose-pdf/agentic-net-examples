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
        const string outputPath = "output_without_tables.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables on each page
            TableAbsorber absorber = new TableAbsorber();

            // Iterate pages using 1‑based indexing
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Extract tables on the current page
                absorber.Visit(doc.Pages[pageIndex]);

                // Copy the TableList because Remove modifies the collection
                var tables = absorber.TableList.ToList();

                // Remove each identified table from the page
                foreach (var table in tables)
                {
                    absorber.Remove(table);
                }
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tables removed. Output saved to '{outputPath}'.");
    }
}