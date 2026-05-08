using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to be bundled into the portfolio
        string[] inputFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        const string outputPortfolio = "portfolio.pdf";

        // Verify that all source files exist before proceeding
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Source file not found: {file}");
                return;
            }
        }

        // Create an empty PDF document that will become the portfolio
        using (Document portfolioDoc = new Document())
        {
            // Ensure the Collection object is instantiated
            if (portfolioDoc.Collection == null)
                portfolioDoc.Collection = new Collection();

            // Add each PDF as an embedded file in the portfolio using FileSpecification
            foreach (string file in inputFiles)
            {
                var fileSpec = new FileSpecification(file, Path.GetFileName(file))
                {
                    Contents = new MemoryStream(File.ReadAllBytes(file))
                };
                portfolioDoc.Collection.Add(fileSpec);
            }

            // Save the resulting portfolio PDF
            portfolioDoc.Save(outputPortfolio);
        }

        Console.WriteLine($"PDF portfolio created successfully at '{outputPortfolio}'.");
    }
}