using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization; // Contains OptimizationOptions

class Program
{
    static void Main()
    {
        const string inputPath  = "portfolio.pdf";          // Existing PDF Portfolio
        const string outputPath = "portfolio_compressed.pdf"; // Desired output file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF Portfolio inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPath))
        {
            // Configure optimization to compress PDF objects
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true   // Enable object compression to reduce file size
            };

            // Apply the optimization settings
            pdfDocument.OptimizeResources(opt);

            // Save the compressed PDF Portfolio to the designated path
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Compressed PDF Portfolio saved to '{outputPath}'.");
    }
}