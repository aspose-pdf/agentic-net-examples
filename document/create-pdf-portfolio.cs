using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to be grouped in the portfolio
        string[] inputFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        const string outputPortfolio = "portfolio.pdf";

        // Verify that all input files exist before proceeding
        foreach (string path in inputFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // Create an empty PDF document that will become the portfolio
        using (Document portfolioDoc = new Document())
        {
            // Ensure a collection exists for portfolio attachments
            if (portfolioDoc.Collection == null)
                portfolioDoc.Collection = new Collection();

            // Add each PDF to the portfolio using FileSpecification
            foreach (string path in inputFiles)
            {
                var fileSpec = new FileSpecification(path, Path.GetFileName(path))
                {
                    // Load the file bytes into the Contents stream
                    Contents = new MemoryStream(File.ReadAllBytes(path))
                };
                portfolioDoc.Collection.Add(fileSpec);
            }

            // Optional: set document title/description metadata
            portfolioDoc.Info.Title = "PDF Portfolio";

            // Save the resulting PDF portfolio
            portfolioDoc.Save(outputPortfolio);
        }

        Console.WriteLine($"PDF portfolio created successfully: {outputPortfolio}");
    }
}
