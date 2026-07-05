using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string portfolioPath = "portfolio.pdf";
        const string nestedPdfPath = "nested.pdf";
        const string outputPath = "updated_portfolio.pdf";

        if (!File.Exists(portfolioPath))
        {
            Console.Error.WriteLine($"Portfolio not found: {portfolioPath}");
            return;
        }
        if (!File.Exists(nestedPdfPath))
        {
            Console.Error.WriteLine($"Nested PDF not found: {nestedPdfPath}");
            return;
        }

        // Load the existing PDF Portfolio
        using (Document doc = new Document(portfolioPath))
        {
            // Create a file specification for the PDF to embed.
            // Use the constructor that accepts the file path and an optional description.
            FileSpecification fileSpec = new FileSpecification(nestedPdfPath, "Embedded PDF document");

            // Set the display name that will appear in the portfolio's attachment list.
            // The correct property for the visible name is 'Name'.
            fileSpec.Name = Path.GetFileName(nestedPdfPath);

            // Add the file specification to the portfolio's embedded files collection.
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the updated portfolio.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Nested PDF added to portfolio. Saved as '{outputPath}'.");
    }
}
