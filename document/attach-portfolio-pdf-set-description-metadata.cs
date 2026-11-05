using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string mainPdfPath = "main.pdf";
        const string portfolioPdfPath = "portfolio.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(mainPdfPath) || !File.Exists(portfolioPdfPath))
        {
            Console.Error.WriteLine("Required PDF files not found.");
            return;
        }

        // Load the main PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(mainPdfPath))
        {
            // Ensure the document has a collection (portfolio) initialized
            if (doc.Collection == null)
                doc.Collection = new Collection();

            // Create a file specification for the portfolio PDF with a custom description
            var fileSpec = new FileSpecification(portfolioPdfPath, "Attached portfolio document")
            {
                // Load the file contents into a memory stream as required by the API
                Contents = new MemoryStream(File.ReadAllBytes(portfolioPdfPath))
            };

            // Add the file specification to the document's collection (portfolio)
            doc.Collection.Add(fileSpec);

            // Set custom metadata (description) using the DocumentInfo indexer
            doc.Info["Description"] = "Custom description for this PDF";

            // Save the updated document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Portfolio attached and metadata set. Saved to '{outputPdfPath}'.");
    }
}
