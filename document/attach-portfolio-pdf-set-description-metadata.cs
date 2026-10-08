using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPdfPath = "target.pdf";      // PDF to which the portfolio will be attached
        const string portfolioPdfPath = "portfolio.pdf"; // PDF file to embed as a portfolio item
        const string outputPdfPath = "output.pdf";      // Resulting PDF with attachment and metadata

        // Verify that input files exist
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }
        if (!File.Exists(portfolioPdfPath))
        {
            Console.Error.WriteLine($"Portfolio PDF not found: {portfolioPdfPath}");
            return;
        }

        // Load the target PDF inside a using block for deterministic disposal
        using (Document doc = new Document(targetPdfPath))
        {
            // Create a FileSpecification for the portfolio PDF with a custom description
            // The constructor reads the file from disk automatically.
            FileSpecification portfolioSpec = new FileSpecification(portfolioPdfPath, "Portfolio attachment");

            // Add the file specification to the document's EmbeddedFiles collection
            doc.EmbeddedFiles.Add(portfolioSpec);

            // Set a custom metadata property named "Description" using the DocumentInfo indexer
            doc.Info["Description"] = "Custom description for the PDF document";

            // Save the modified PDF. Since the output is a PDF, no SaveOptions are required.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with portfolio attachment and custom metadata: {outputPdfPath}");
    }
}
