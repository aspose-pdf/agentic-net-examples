using System;
using System.IO;
using Aspose.Pdf;

class PortfolioCreator
{
    static void Main()
    {
        // Input PDF that will become a portfolio
        const string sourcePdf = "source.pdf";

        // Files to embed into the portfolio
        string[] filesToEmbed = { "file1.txt", "image1.png", "document2.docx" };

        // Output PDF portfolio
        const string outputPdf = "portfolio.pdf";

        // Verify source PDF exists
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdf}");
            return;
        }

        // Verify each file to embed exists
        foreach (string f in filesToEmbed)
        {
            if (!File.Exists(f))
            {
                Console.Error.WriteLine($"File to embed not found: {f}");
                return;
            }
        }

        // Load the source PDF
        using (Document doc = new Document(sourcePdf))
        {
            // Ensure the document has a Collection object (creates a portfolio container)
            if (doc.Collection == null)
                doc.Collection = new Collection();

            // Add each file as an embedded file specification
            foreach (string filePath in filesToEmbed)
            {
                string description = Path.GetFileName(filePath);
                // Create a file specification for the embedded file
                var fileSpec = new FileSpecification(description, description);
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(filePath));
                // Add the specification to the document's collection (portfolio)
                doc.Collection.Add(fileSpec);
            }

            // Save the resulting PDF as a portfolio
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Portfolio PDF created: {outputPdf}");
    }
}
