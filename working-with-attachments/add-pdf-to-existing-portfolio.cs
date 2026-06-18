using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the existing PDF portfolio, the PDF to embed, and the output file
        const string portfolioPath = "portfolio.pdf";
        const string fileToEmbedPath = "nested.pdf";
        const string outputPath = "updated_portfolio.pdf";

        // Verify that both source files exist
        if (!File.Exists(portfolioPath))
        {
            Console.Error.WriteLine($"Portfolio file not found: {portfolioPath}");
            return;
        }
        if (!File.Exists(fileToEmbedPath))
        {
            Console.Error.WriteLine($"File to embed not found: {fileToEmbedPath}");
            return;
        }

        // Open the existing portfolio PDF
        using (Document portfolioDoc = new Document(portfolioPath))
        {
            // Create a FileSpecification for the PDF to be added as an embedded file
            using (FileStream embedStream = File.OpenRead(fileToEmbedPath))
            {
                FileSpecification fileSpec = new FileSpecification(embedStream, Path.GetFileName(fileToEmbedPath));

                // Add the file specification to the portfolio's embedded files collection
                portfolioDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the updated portfolio PDF
            portfolioDoc.Save(outputPath);
        }

        Console.WriteLine($"Embedded '{fileToEmbedPath}' into portfolio. Saved as '{outputPath}'.");
    }
}