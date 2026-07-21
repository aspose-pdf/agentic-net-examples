using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the PDFs that will be bundled into the portfolio
        string[] pdfFiles = { "report1.pdf", "report2.pdf", "appendix.pdf" };
        const string portfolioPath = "portfolio.pdf";

        // Verify that all source files exist before creating the portfolio
        foreach (string file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Source file not found: {file}");
                return;
            }
        }

        // Create an empty PDF document that will act as the portfolio container
        using (Document portfolioDoc = new Document())
        {
            // Ensure a collection exists for embedded files (portfolio)
            if (portfolioDoc.Collection == null)
                portfolioDoc.Collection = new Collection();

            // Add each PDF file to the portfolio using FileSpecification
            foreach (string file in pdfFiles)
            {
                var fileSpec = new FileSpecification(file, Path.GetFileName(file));
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(file));
                portfolioDoc.Collection.Add(fileSpec);
            }

            // Optional: set document metadata (title/description)
            portfolioDoc.Info.Title = "PDF Portfolio";

            // Save the resulting PDF portfolio
            portfolioDoc.Save(portfolioPath);
        }

        Console.WriteLine($"PDF portfolio created at '{portfolioPath}'.");
    }
}
