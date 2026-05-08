using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string portfolioPath1 = "portfolio1.pdf";
        const string portfolioPath2 = "portfolio2.pdf";
        const string outputPath      = "merged_portfolio.pdf";

        // Verify that both source files exist
        if (!File.Exists(portfolioPath1) || !File.Exists(portfolioPath2))
        {
            Console.Error.WriteLine("One or both input PDF portfolios were not found.");
            return;
        }

        // Merge the two PDF portfolios. The static MergeDocuments method
        // combines the documents while preserving embedded files and their metadata.
        Document mergedDocument = Document.MergeDocuments(portfolioPath1, portfolioPath2);

        // Save the merged result
        mergedDocument.Save(outputPath);
        Console.WriteLine($"Merged PDF portfolio saved to '{outputPath}'.");
    }
}