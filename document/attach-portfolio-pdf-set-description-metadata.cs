using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the main PDF, the portfolio PDF to attach, and the output PDF
        const string mainPdfPath      = "input.pdf";
        const string portfolioPdfPath = "portfolio.pdf";
        const string outputPdfPath    = "output_with_portfolio.pdf";

        // Verify that source files exist
        if (!File.Exists(mainPdfPath) || !File.Exists(portfolioPdfPath))
        {
            Console.Error.WriteLine("Required PDF files not found.");
            return;
        }

        // Load the main PDF document (lifecycle: load)
        using (Document doc = new Document(mainPdfPath))
        {
            // Create a FileSpecification for the portfolio PDF
            FileSpecification portfolioSpec = new FileSpecification(portfolioPdfPath);

            // Attach the portfolio PDF as an embedded file (portfolio)
            // Use the overload that accepts a key and the FileSpecification
            doc.EmbeddedFiles.Add("Portfolio.pdf", portfolioSpec);

            // Set custom description metadata (XMP metadata)
            doc.Metadata.Add("Description", "This PDF contains an attached portfolio document.");

            // Optionally set a standard document info title as well
            doc.Info.Title = "PDF with Portfolio Attachment";

            // Save the modified document (lifecycle: save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Portfolio attached and metadata set. Output saved to '{outputPdfPath}'.");
    }
}