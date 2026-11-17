using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to files that will be included in the portfolio
        const string file1 = "sample1.pdf";
        const string file2 = "sample2.pdf";
        const string outputPortfolio = "portfolio.pdf";

        // Verify source files exist
        if (!File.Exists(file1) || !File.Exists(file2))
        {
            Console.Error.WriteLine("One or more source files are missing.");
            return;
        }

        // Create a new (empty) PDF document that will become a portfolio
        using (Document portfolioDoc = new Document())
        {
            // Initialise the collection that holds embedded files (portfolio entries)
            if (portfolioDoc.Collection == null)
                portfolioDoc.Collection = new Collection();

            // Helper to add a file to the portfolio
            void AddFileToPortfolio(string path)
            {
                // Create a FileSpecification for the file. The second argument is a description –
                // we use the file name for simplicity.
                var fileSpec = new FileSpecification(path, Path.GetFileName(path))
                {
                    // Load the file contents into a memory stream; this is required for the
                    // embedded file representation.
                    Contents = new MemoryStream(File.ReadAllBytes(path))
                };
                portfolioDoc.Collection.Add(fileSpec);
            }

            AddFileToPortfolio(file1);
            AddFileToPortfolio(file2);

            // Optional: set a title for the portfolio document
            portfolioDoc.Info.Title = "PDF Portfolio";

            // Save the portfolio as a PDF file
            portfolioDoc.Save(outputPortfolio);
        }

        Console.WriteLine($"PDF Portfolio created at '{outputPortfolio}'.");
    }
}
