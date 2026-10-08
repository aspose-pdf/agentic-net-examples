using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string portfolioPath = "portfolio.pdf";
        const string fileToAddPath = "add.pdf";
        const string outputPath = "portfolio_with_nested.pdf";

        if (!File.Exists(portfolioPath))
        {
            Console.Error.WriteLine($"Portfolio file not found: {portfolioPath}");
            return;
        }

        if (!File.Exists(fileToAddPath))
        {
            Console.Error.WriteLine($"File to embed not found: {fileToAddPath}");
            return;
        }

        try
        {
            // Open the existing PDF portfolio
            using (Document portfolioDoc = new Document(portfolioPath))
            {
                // Read the PDF to embed
                byte[] fileBytes = File.ReadAllBytes(fileToAddPath);

                // Create a FileSpecification with a hierarchical name (folder + file)
                var fileSpec = new FileSpecification("Attachments/add.pdf", "Embedded PDF");
                fileSpec.Contents = new MemoryStream(fileBytes);

                // Add the specification to the portfolio's embedded files collection
                portfolioDoc.EmbeddedFiles.Add(fileSpec);

                // Save the updated portfolio
                portfolioDoc.Save(outputPath);
            }

            Console.WriteLine($"Nested PDF added. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
