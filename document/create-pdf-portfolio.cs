using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // PDFs to be grouped in the portfolio
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        const string outputPath = "portfolio.pdf";

        // Verify that all source files exist
        foreach (var file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        // Create an empty PDF that will serve as the portfolio container
        using (Document portfolioDoc = new Document())
        {
            // Ensure the Collection object exists – it holds the embedded files for a portfolio
            if (portfolioDoc.Collection == null)
                portfolioDoc.Collection = new Collection();

            // Add each PDF as a FileSpecification (embedded file) to the collection
            foreach (var file in pdfFiles)
            {
                var fileSpec = new FileSpecification(file, Path.GetFileName(file))
                {
                    // Load the file content; this is required for the portfolio entry
                    Contents = new MemoryStream(File.ReadAllBytes(file))
                };
                portfolioDoc.Collection.Add(fileSpec);
            }

            // Optional: set basic document metadata
            portfolioDoc.Info.Title = "Combined PDF Portfolio";
            portfolioDoc.Info.Author = "Aspose.Pdf Example";

            // Save the portfolio PDF
            portfolioDoc.Save(outputPath);
        }

        Console.WriteLine($"Portfolio created at '{outputPath}'.");
    }
}
