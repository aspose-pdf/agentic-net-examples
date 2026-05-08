using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "portfolio.pdf";
        const string outputPath = "portfolio_compressed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF Portfolio
        using (Document doc = new Document(inputPath))
        {
            // Enable compression of PDF objects to reduce file size
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true
            };
            doc.OptimizeResources(opt);

            // Save the compressed PDF Portfolio
            doc.Save(outputPath);
        }

        Console.WriteLine($"Compressed PDF Portfolio saved to '{outputPath}'.");
    }
}