using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        // Paths for the source PDF Portfolio and the compressed output file.
        const string inputPortfolioPath  = "portfolio_input.pdf";
        const string outputCompressedPath = "portfolio_compressed.pdf";

        // Verify that the source file exists.
        if (!File.Exists(inputPortfolioPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPortfolioPath}");
            return;
        }

        // Load the existing PDF Portfolio.
        using (Document pdfPortfolio = new Document(inputPortfolioPath))
        {
            // Create optimization options and enable object compression.
            OptimizationOptions optOptions = new OptimizationOptions
            {
                CompressObjects = true   // Compress PDF objects to reduce file size.
            };

            // Apply the optimization to the document.
            pdfPortfolio.OptimizeResources(optOptions);

            // Save the optimized (compressed) PDF Portfolio to the designated output path.
            pdfPortfolio.Save(outputCompressedPath);
        }

        Console.WriteLine($"Compressed PDF Portfolio saved to '{outputCompressedPath}'.");
    }
}