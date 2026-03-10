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
        const string outputPath = "output_no_tables.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Copy the list because Remove modifies the original collection
            var tables = absorber.TableList.ToList();

            // Remove each detected table while preserving other content
            foreach (var table in tables)
            {
                absorber.Remove(table);
            }

            // Save the modified PDF (still a PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All tables removed. Saved to '{outputPath}'.");
    }
}