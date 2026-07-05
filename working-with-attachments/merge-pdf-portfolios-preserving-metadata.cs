using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string portfolio1Path = "portfolio1.pdf";
        const string portfolio2Path = "portfolio2.pdf";
        const string mergedOutputPath = "merged_portfolio.pdf";

        if (!File.Exists(portfolio1Path))
        {
            Console.Error.WriteLine($"File not found: {portfolio1Path}");
            return;
        }
        if (!File.Exists(portfolio2Path))
        {
            Console.Error.WriteLine($"File not found: {portfolio2Path}");
            return;
        }

        try
        {
            // Load the two portfolio PDFs
            using (Document doc1 = new Document(portfolio1Path))
            using (Document doc2 = new Document(portfolio2Path))
            {
                // Merge the second portfolio into the first.
                // Document.Merge preserves embedded files (portfolio items) and their metadata.
                doc1.Merge(doc2);

                // Save the merged result.
                doc1.Save(mergedOutputPath);
            }

            Console.WriteLine($"Portfolios merged successfully: {mergedOutputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge: {ex.Message}");
        }
    }
}