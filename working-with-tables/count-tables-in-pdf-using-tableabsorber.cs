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

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber instance to search for tables
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // The TableList collection contains all found tables
            int tableCount = absorber.TableList.Count;

            Console.WriteLine($"Number of tables found: {tableCount}");

            // Save the (potentially modified) document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed document saved to '{outputPath}'.");
    }
}