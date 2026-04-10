using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string portfolio1Path = "portfolio1.pdf";
        const string portfolio2Path = "portfolio2.pdf";
        const string outputPath     = "merged_portfolio.pdf";

        // Verify that both source files exist.
        if (!File.Exists(portfolio1Path) || !File.Exists(portfolio2Path))
        {
            Console.Error.WriteLine("One or both input PDF portfolios were not found.");
            return;
        }

        // Load the two PDF portfolios.
        using (Document doc1 = new Document(portfolio1Path))
        using (Document doc2 = new Document(portfolio2Path))
        {
            // Merge the second portfolio into the first.
            // The core Merge method retains embedded files (portfolio items) and their metadata.
            doc1.Merge(doc2);

            // Save the combined portfolio.
            doc1.Save(outputPath);
        }

        Console.WriteLine($"Merged PDF portfolio saved to '{outputPath}'.");
    }
}