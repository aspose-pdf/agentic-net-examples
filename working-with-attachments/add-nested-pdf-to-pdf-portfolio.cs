using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the existing portfolio PDF, the PDF to embed, and the output file
        const string portfolioPath = "portfolio.pdf";
        const string nestedPdfPath = "nested.pdf";
        const string outputPath    = "portfolio_with_nested.pdf";

        // Ensure source files exist
        if (!File.Exists(portfolioPath))
        {
            Console.Error.WriteLine($"Portfolio file not found: {portfolioPath}");
            return;
        }
        if (!File.Exists(nestedPdfPath))
        {
            Console.Error.WriteLine($"Nested PDF file not found: {nestedPdfPath}");
            return;
        }

        // Open the existing PDF portfolio
        using (Document doc = new Document(portfolioPath))
        {
            // Create a FileSpecification using the constructor (filePath, description)
            FileSpecification fileSpec = new FileSpecification(nestedPdfPath, "Nested PDF document");
            // Optionally set a display name for the attachment
            fileSpec.Name = Path.GetFileName(nestedPdfPath);

            // Add the file specification to the portfolio's embedded files collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the updated portfolio
            doc.Save(outputPath);
        }

        Console.WriteLine($"Nested PDF added to portfolio. Saved as '{outputPath}'.");
    }
}