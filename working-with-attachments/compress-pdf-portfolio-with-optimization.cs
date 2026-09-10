using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPortfolioPath  = "portfolio_input.pdf";   // existing PDF Portfolio
        const string outputPortfolioPath = "portfolio_output.pdf";  // destination with compression

        if (!File.Exists(inputPortfolioPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPortfolioPath}");
            return;
        }

        // Load the existing PDF Portfolio
        using (Document pdfPortfolio = new Document(inputPortfolioPath))
        {
            // Enable compression of PDF objects to reduce file size
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true   // compress objects in the PDF
            };
            pdfPortfolio.OptimizeResources(opt);

            // Save the compressed PDF Portfolio to the designated output path
            pdfPortfolio.Save(outputPortfolioPath);
        }

        Console.WriteLine($"Compressed PDF Portfolio saved to '{outputPortfolioPath}'.");
    }
}