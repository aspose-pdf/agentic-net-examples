using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the existing PDF portfolio, the Word document to add, and the output PDF
        const string portfolioPath = "portfolio.pdf";
        const string wordDocPath   = "document.docx";
        const string outputPath    = "portfolio_with_word.pdf";

        // Verify that the source files exist
        if (!File.Exists(portfolioPath))
        {
            Console.Error.WriteLine($"PDF not found: {portfolioPath}");
            return;
        }
        if (!File.Exists(wordDocPath))
        {
            Console.Error.WriteLine($"Word document not found: {wordDocPath}");
            return;
        }

        // Load the PDF (or existing portfolio) inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(portfolioPath))
        {
            // Create a FileSpecification for the Word document.
            // The constructor that takes a file path creates an embedded file specification.
            FileSpecification wordSpec = new FileSpecification(wordDocPath);
            // Optional: set a description that will appear in the portfolio UI.
            wordSpec.Description = Path.GetFileName(wordDocPath);

            // Add the Word document to the PDF portfolio collection.
            pdfDoc.Collection.Add(wordSpec);

            // Save the updated PDF portfolio
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Word document added to portfolio and saved as '{outputPath}'.");
    }
}
