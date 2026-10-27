using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string portfolioPath = "portfolio.pdf";          // existing PDF portfolio (optional)
        const string wordPath      = "document.docx";          // Word file to embed
        const string outputPath    = "portfolio_with_word.pdf"; // result PDF

        if (!File.Exists(wordPath))
        {
            Console.Error.WriteLine($"Word file not found: {wordPath}");
            return;
        }

        // Load existing portfolio if it exists; otherwise create a new empty PDF.
        using (Document pdf = File.Exists(portfolioPath) ? new Document(portfolioPath) : new Document())
        {
            // Create a FileSpecification for the Word document.
            var fileSpec = new FileSpecification(wordPath, Path.GetFileName(wordPath));
            // Optional: set the modification date of the attachment.
            fileSpec.Params.ModDate = DateTime.UtcNow;

            // Add the FileSpecification to the PDF portfolio collection.
            pdf.Collection.Add(fileSpec);

            // Save the updated PDF portfolio.
            pdf.Save(outputPath);
        }

        Console.WriteLine($"Word document added to portfolio: {outputPath}");
    }
}
