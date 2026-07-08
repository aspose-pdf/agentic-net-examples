using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string mainPdfPath = "main.pdf";               // PDF to which we attach the portfolio
        const string portfolioPdfPath = "portfolio.pdf";     // PDF portfolio file to embed
        const string outputPath = "output_with_portfolio.pdf";

        // Verify source files exist
        if (!File.Exists(mainPdfPath))
        {
            Console.Error.WriteLine($"Main PDF not found: {mainPdfPath}");
            return;
        }
        if (!File.Exists(portfolioPdfPath))
        {
            Console.Error.WriteLine($"Portfolio PDF not found: {portfolioPdfPath}");
            return;
        }

        // Load the main PDF document (lifecycle rule: load)
        using (Document doc = new Document(mainPdfPath))
        {
            // Create a file specification for the portfolio PDF
            FileSpecification fileSpec = new FileSpecification(portfolioPdfPath);

            // Set custom description metadata for the embedded file
            fileSpec.Description = "Custom description for the attached portfolio file.";

            // Attach the portfolio as an embedded file (portfolio)
            doc.EmbeddedFiles.Add(fileSpec);

            // Optional: set document‑level metadata
            doc.Info.Title = "Document with Portfolio Attachment";
            doc.Info.Subject = "Demonstrates attaching a PDF portfolio";

            // Save the updated document (lifecycle rule: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Portfolio attached and saved to '{outputPath}'.");
    }
}