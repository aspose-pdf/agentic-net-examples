using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "portfolio.pdf";

        using (Document doc = new Document())
        {
            // Ensure the document has a collection for portfolio files
            if (doc.Collection == null)
                doc.Collection = new Collection();

            // A portfolio needs at least one page; add a blank page
            doc.Pages.Add();

            // Files to attach to the portfolio
            string txtFile = "sample.txt";
            string imgFile = "image.png";

            AddFileToPortfolio(doc, txtFile);
            AddFileToPortfolio(doc, imgFile);

            // Save the document as a PDF Portfolio
            doc.Save(outputPath);
        }

        Console.WriteLine($"Portfolio saved to '{outputPath}'.");
    }

    private static void AddFileToPortfolio(Document doc, string filePath)
    {
        if (!File.Exists(filePath))
            return;

        // Create a FileSpecification for the file and load its contents
        var fileSpec = new FileSpecification(filePath, Path.GetFileName(filePath))
        {
            Contents = new MemoryStream(File.ReadAllBytes(filePath))
        };

        // Add the specification to the document's collection (portfolio)
        doc.Collection.Add(fileSpec);
    }
}
