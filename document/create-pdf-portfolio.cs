using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths of the PDFs to be bundled into the portfolio
        string[] sourceFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        const string portfolioPath = "portfolio.pdf";

        // Verify that all source files exist before proceeding
        foreach (string file in sourceFiles)
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
            // Ensure the document has a Collection (portfolio) object
            if (portfolioDoc.Collection == null)
                portfolioDoc.Collection = new Collection();

            // Add each PDF as an embedded file in the portfolio
            foreach (string file in sourceFiles)
            {
                var fileSpec = new FileSpecification(file, Path.GetFileName(file))
                {
                    // Load the file content into the specification
                    Contents = new MemoryStream(File.ReadAllBytes(file))
                };
                portfolioDoc.Collection.Add(fileSpec);
            }

            // Optional: set document metadata
            portfolioDoc.Info.Title = "PDF Portfolio";

            // Save the resulting PDF portfolio
            portfolioDoc.Save(portfolioPath);
        }

        Console.WriteLine($"PDF portfolio created at '{portfolioPath}'.");
    }
}
