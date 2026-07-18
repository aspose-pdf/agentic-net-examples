using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string portfolioPath1 = "portfolio1.pdf";
        const string portfolioPath2 = "portfolio2.pdf";
        const string outputPath = "merged_portfolio.pdf";

        if (!File.Exists(portfolioPath1) || !File.Exists(portfolioPath2))
        {
            Console.Error.WriteLine("One or both input portfolio files were not found.");
            return;
        }

        // Load the two source portfolio PDFs
        using (Document doc1 = new Document(portfolioPath1))
        using (Document doc2 = new Document(portfolioPath2))
        {
            // Create an empty document to receive the merged result
            using (Document merged = new Document())
            {
                // Merge the two source documents into the empty one
                merged.Merge(doc1, doc2);

                // Preserve standard document metadata from the first portfolio
                merged.Info.Title = doc1.Info.Title;
                merged.Info.Author = doc1.Info.Author;
                merged.Info.Subject = doc1.Info.Subject;
                merged.Info.Keywords = doc1.Info.Keywords;
                merged.Info.Creator = doc1.Info.Creator;
                merged.Info.Producer = doc1.Info.Producer;
                merged.Info.ModDate = doc1.Info.ModDate;
                merged.Info.CreationDate = doc1.Info.CreationDate;

                // Preserve XMP metadata (custom metadata) from the first portfolio
                using (MemoryStream xmpStream = new MemoryStream())
                {
                    doc1.GetXmpMetadata(xmpStream);
                    xmpStream.Position = 0;
                    merged.SetXmpMetadata(xmpStream);
                }

                // Save the merged portfolio PDF
                merged.Save(outputPath);
            }
        }

        Console.WriteLine($"Merged portfolio saved to '{outputPath}'.");
    }
}