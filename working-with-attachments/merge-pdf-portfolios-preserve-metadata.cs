using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string portfolioPath1 = "portfolio1.pdf";
        const string portfolioPath2 = "portfolio2.pdf";
        const string outputPath     = "merged_portfolio.pdf";

        if (!File.Exists(portfolioPath1) || !File.Exists(portfolioPath2))
        {
            Console.Error.WriteLine("One or both input portfolio files not found.");
            return;
        }

        // Load the two PDF portfolios
        using (Document doc1 = new Document(portfolioPath1))
        using (Document doc2 = new Document(portfolioPath2))
        {
            // Append pages from the second portfolio (if any)
            doc1.Pages.Add(doc2.Pages);

            // Preserve embedded files (portfolio items) and their metadata
            foreach (FileSpecification fileSpec in doc2.EmbeddedFiles)
            {
                // Add each embedded file from doc2 to doc1. The FileSpecification object
                // already contains its metadata (Description, CreationDate, etc.).
                doc1.EmbeddedFiles.Add(fileSpec);
            }

            // Save the merged portfolio
            doc1.Save(outputPath);
        }

        Console.WriteLine($"Merged portfolio saved to '{outputPath}'.");
    }
}
