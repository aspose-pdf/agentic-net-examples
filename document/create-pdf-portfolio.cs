using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Files to be included in the portfolio
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        const string outputPath = "portfolio.pdf";

        // Verify that all source files exist
        foreach (var file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Source file not found: {file}");
                return;
            }
        }

        // Create a new PDF document that will serve as the portfolio container
        using (Document portfolioDoc = new Document())
        {
            // Initialise the collection that holds embedded files (portfolio)
            if (portfolioDoc.Collection == null)
                portfolioDoc.Collection = new Collection();

            // Add each PDF as an embedded file in the portfolio
            foreach (var file in pdfFiles)
            {
                var fileSpec = new FileSpecification(file, Path.GetFileName(file))
                {
                    // The file content must be supplied as a stream
                    Contents = new MemoryStream(File.ReadAllBytes(file))
                };
                portfolioDoc.Collection.Add(fileSpec);
            }

            // Optional: set document metadata
            portfolioDoc.Info.Title = "My PDF Portfolio";

            // Save the portfolio; the result is a single PDF containing the embedded PDFs
            portfolioDoc.Save(outputPath);
        }

        Console.WriteLine($"Portfolio created successfully at '{outputPath}'.");
    }
}
