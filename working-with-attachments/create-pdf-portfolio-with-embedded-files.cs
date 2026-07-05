using System;
using System.IO;
using Aspose.Pdf;

class PortfolioCreator
{
    static void Main()
    {
        // Input PDF that will become a portfolio
        const string sourcePdf = "source.pdf";

        // Output PDF portfolio file
        const string portfolioPdf = "portfolio.pdf";

        // Files to embed into the portfolio
        string[] filesToEmbed = { "doc1.pdf", "image1.png", "notes.txt" };

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdf}");
            return;
        }

        // Verify that all files to embed exist
        foreach (var f in filesToEmbed)
        {
            if (!File.Exists(f))
            {
                Console.Error.WriteLine($"File to embed not found: {f}");
                return;
            }
        }

        // Open the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(sourcePdf))
        {
            // Ensure the document has a Collection (portfolio) object
            if (doc.Collection == null)
                doc.Collection = new Collection();

            // Add each file as an embedded file (portfolio entry)
            foreach (var filePath in filesToEmbed)
            {
                // Use the file name (without path) as the entry name in the portfolio
                string entryName = Path.GetFileName(filePath);

                // Create a FileSpecification for the embedded file
                var fileSpec = new FileSpecification(entryName, entryName);
                // Load the file bytes into a MemoryStream and assign to Contents
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(filePath));

                // Add the specification to the document's collection (portfolio)
                doc.Collection.Add(fileSpec);
            }

            // Optionally set a title for the portfolio document
            doc.Info.Title = "PDF Portfolio";

            // Save the resulting PDF portfolio
            doc.Save(portfolioPdf);
        }

        Console.WriteLine($"Portfolio created: {portfolioPdf}");
    }
}
