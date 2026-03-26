using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "portfolio.pdf";
        string[] inputFiles = new string[] { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        // Verify that all source files exist before creating the portfolio
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        // Create an empty PDF document that will act as the portfolio container
        using (Document portfolioDoc = new Document())
        {
            // Initialise the collection that holds embedded files (the portfolio)
            if (portfolioDoc.Collection == null)
                portfolioDoc.Collection = new Collection();

            // Add each PDF to the portfolio as a FileSpecification
            foreach (string file in inputFiles)
            {
                // The file name is used as the description inside the portfolio
                var fileSpec = new FileSpecification(file, Path.GetFileName(file))
                {
                    // Load the file content into a memory stream
                    Contents = new MemoryStream(File.ReadAllBytes(file))
                };

                portfolioDoc.Collection.Add(fileSpec);
            }

            // Optional: set a title for the portfolio document
            portfolioDoc.Info.Title = "Combined PDF Portfolio";

            // Save the resulting PDF portfolio
            portfolioDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF portfolio created: {outputPath}");
    }
}
