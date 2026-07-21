using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string portfolioPath = "portfolio.pdf";   // Existing PDF Portfolio
        const string nestedPdfPath = "nested.pdf";      // PDF to embed as a nested item
        const string outputPath   = "portfolio_updated.pdf";

        // Verify source files exist
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

        // Open the existing portfolio PDF
        using (Document doc = new Document(portfolioPath))
        {
            // Ensure the document has a Collection (Portfolio) object.
            // If the PDF was not originally a portfolio, create one.
            if (doc.Collection == null)
            {
                doc.Collection = new Collection();
            }

            // Create a file specification for the PDF to be embedded.
            // The constructor can take a file path; the file will be read and stored.
            FileSpecification fileSpec = new FileSpecification(nestedPdfPath);
            fileSpec.Description = "Embedded PDF document";

            // Add the file specification to the portfolio collection.
            // Collection inherits from EmbeddedFileCollection, so Add() is available.
            doc.Collection.Add(fileSpec);

            // Save the updated PDF portfolio.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Nested PDF added to portfolio. Saved as '{outputPath}'.");
    }
}