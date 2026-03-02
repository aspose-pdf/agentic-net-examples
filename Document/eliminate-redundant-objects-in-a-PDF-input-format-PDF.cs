using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "optimized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create an optimization strategy with all options enabled
            OptimizationOptions options = OptimizationOptions.All();

            // Remove unused resources, merge duplicate objects, etc.
            pdfDoc.OptimizeResources(options);

            // Save the optimized PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}