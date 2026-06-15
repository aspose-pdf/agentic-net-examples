using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths of the PDFs to be bundled into the portfolio
        string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        const string outputPortfolio = "portfolio.pdf";

        // Ensure all source files exist before proceeding
        foreach (var file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Source file not found: {file}");
                return;
            }
        }

        // Create an empty PDF document that will become the portfolio container
        using (Document portfolioDoc = new Document())
        {
            // Initialise the collection that holds embedded files (portfolio entries)
            if (portfolioDoc.Collection == null)
                portfolioDoc.Collection = new Collection();

            // Add each PDF as an embedded file in the portfolio
            foreach (var file in pdfFiles)
            {
                var fileSpec = new FileSpecification(file, Path.GetFileName(file))
                {
                    // Load the file bytes into the Contents stream of the specification
                    Contents = new MemoryStream(File.ReadAllBytes(file))
                };
                portfolioDoc.Collection.Add(fileSpec);
            }

            // Save the resulting portfolio PDF
            portfolioDoc.Save(outputPortfolio);
        }

        Console.WriteLine($"PDF portfolio created at '{outputPortfolio}'.");
    }
}