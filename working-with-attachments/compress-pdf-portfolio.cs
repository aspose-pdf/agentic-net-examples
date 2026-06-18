using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath  = "portfolio_input.pdf";   // path to the populated PDF Portfolio
        const string outputPath = "portfolio_output.pdf";  // desired output path

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF Portfolio, modify if needed, and save with compression.
        using (Document doc = new Document(inputPath))
        {
            // Enable object compression to reduce file size.
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true   // compress PDF objects
            };

            // Apply the optimization settings.
            doc.OptimizeResources(opt);

            // Save the compressed PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Compressed PDF Portfolio saved to '{outputPath}'.");
    }
}