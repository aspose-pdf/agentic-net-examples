using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "optimized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Remove redundant objects: unused resources, duplicate objects, etc.
            pdfDoc.OptimizeResources();

            // Save the optimized PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}