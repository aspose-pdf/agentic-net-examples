using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "optimized_output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Enable resource merging when identical streams are added
            doc.OptimizeSize = true;

            // Linearize the document for faster web viewing (first page loads quickly)
            doc.Optimize();

            // Remove unused objects, merge duplicate resources, and compress streams
            doc.OptimizeResources();

            // Save the optimized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}