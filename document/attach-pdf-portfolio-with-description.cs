using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPdfPath = "target.pdf";      // PDF to which the portfolio will be attached
        const string portfolioPdfPath = "portfolio.pdf"; // PDF that will become the portfolio file
        const string outputPdfPath = "output.pdf";      // Resulting PDF with attachment

        // Verify that both source files exist
        if (!File.Exists(targetPdfPath) || !File.Exists(portfolioPdfPath))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        // Load the target PDF document
        using (Document doc = new Document(targetPdfPath))
        {
            // Create a file specification for the portfolio PDF with a custom description.
            FileSpecification portfolioSpec = new FileSpecification(
                portfolioPdfPath,
                "Attached portfolio document containing supplementary information.");

            // Add the file specification to the document's embedded files collection.
            doc.EmbeddedFiles.Add(portfolioSpec);

            // Set document‑level metadata (optional but demonstrates custom description handling).
            doc.Info.Title = "Document with Portfolio Attachment";
            doc.Info.Subject = "Demonstration of PDF portfolio attachment";
            doc.Info.Keywords = "Aspose.Pdf, portfolio, attachment";

            // Save the modified PDF. No PdfSaveOptions with InterruptMonitor are used.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Portfolio attached and saved to '{outputPdfPath}'.");
    }
}