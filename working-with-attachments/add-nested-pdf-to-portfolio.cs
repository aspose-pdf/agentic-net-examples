using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string portfolioPath = "portfolio.pdf";   // existing PDF Portfolio
        const string fileToAdd    = "nested.pdf";      // PDF to embed as a nested item
        const string outputPath   = "updated_portfolio.pdf";

        // Verify that both source files exist
        if (!File.Exists(portfolioPath))
        {
            Console.Error.WriteLine($"Portfolio file not found: {portfolioPath}");
            return;
        }
        if (!File.Exists(fileToAdd))
        {
            Console.Error.WriteLine($"File to embed not found: {fileToAdd}");
            return;
        }

        // Load the existing portfolio PDF
        using (Document doc = new Document(portfolioPath))
        {
            // Create a file specification for the PDF to be embedded
            FileSpecification fileSpec = new FileSpecification(fileToAdd);
            fileSpec.Description = "Nested PDF document";

            // Add the file specification to the portfolio's embedded files collection.
            // Use the file name as the key.
            doc.EmbeddedFiles.Add(Path.GetFileName(fileToAdd), fileSpec);

            // Save the updated portfolio
            doc.Save(outputPath);
        }

        Console.WriteLine($"Embedded '{fileToAdd}' into portfolio. Saved as '{outputPath}'.");
    }
}