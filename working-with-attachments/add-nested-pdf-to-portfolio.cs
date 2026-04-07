using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string portfolioPath = "portfolio.pdf";   // Existing PDF Portfolio
        const string nestedPdfPath = "nested.pdf";      // PDF to embed
        const string outputPath   = "portfolio_with_nested.pdf";

        // Verify source files exist
        if (!File.Exists(portfolioPath))
        {
            Console.Error.WriteLine($"Portfolio file not found: {portfolioPath}");
            return;
        }
        if (!File.Exists(nestedPdfPath))
        {
            Console.Error.WriteLine($"Nested PDF not found: {nestedPdfPath}");
            return;
        }

        // Open the existing portfolio PDF
        using (Document doc = new Document(portfolioPath))
        {
            // Create a file specification for the PDF to be embedded
            // The constructor accepts a file path and creates the necessary
            // embedded file entry.
            FileSpecification fileSpec = new FileSpecification(nestedPdfPath);

            // Add the file specification to the portfolio's embedded files collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the updated portfolio
            doc.Save(outputPath);
        }

        Console.WriteLine($"Nested PDF added. Output saved to '{outputPath}'.");
    }
}