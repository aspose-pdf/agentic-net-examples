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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize TableAbsorber to locate tables
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Create a copy of the TableList because Remove modifies the collection
            var tables = absorber.TableList.ToList();

            // Remove each absorbed table from its page
            foreach (var table in tables)
            {
                absorber.Remove(table);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All tables removed. Saved to '{outputPath}'.");
    }
}