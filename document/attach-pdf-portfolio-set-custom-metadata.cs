using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string mainPdfPath      = "main.pdf";          // PDF to which the portfolio will be attached
        const string portfolioPdfPath = "portfolio.pdf";     // PDF file to embed as a portfolio item
        const string outputPdfPath    = "output_with_portfolio.pdf";

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

        // Load the main document (lifecycle: load)
        using (Document doc = new Document(mainPdfPath))
        {
            // Create a file specification for the portfolio PDF
            // The constructor accepts the file path; the file will be embedded.
            FileSpecification fileSpec = new FileSpecification(portfolioPdfPath);

            // Optional: set a custom description for the embedded file
            // (FileSpecification.Description is the appropriate property)
            fileSpec.Description = "Attached portfolio document";

            // Add the file specification to the document's embedded files collection
            // (EmbeddedFileCollection.Add adds the file to the PDF portfolio)
            doc.EmbeddedFiles.Add(fileSpec);

            // Set custom metadata on the PDF document
            // Example: set the title and add a custom key/value pair
            doc.Info.Title = "PDF with Portfolio Attachment";
            doc.Info.Add("CustomDescription", "This PDF contains an attached portfolio file.");

            // Save the modified document (lifecycle: save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attached portfolio: {outputPdfPath}");
    }
}