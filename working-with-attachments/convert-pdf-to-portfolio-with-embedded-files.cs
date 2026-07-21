using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF (regular PDF) and output PDF (portfolio)
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "portfolio.pdf";

        // Files to embed into the portfolio
        string[] filesToEmbed = new string[]
        {
            "attachment1.docx",
            "attachment2.xlsx",
            "attachment3.jpg"
        };

        // Verify input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        foreach (string f in filesToEmbed)
        {
            if (!File.Exists(f))
            {
                Console.Error.WriteLine($"Attachment file not found: {f}");
                return;
            }
        }

        // Load the regular PDF, add embedded files, and save as a portfolio
        using (Document doc = new Document(inputPdfPath))
        {
            // Adding embedded files automatically converts the document to a PDF Portfolio.
            // (In recent Aspose.Pdf versions the IsPortfolio property is obsolete; the presence of embedded files defines a portfolio.)

            foreach (string filePath in filesToEmbed)
            {
                // Create a file specification for the embedded file
                FileSpecification embeddedFile = new FileSpecification(filePath)
                {
                    // Optional description for the embedded file
                    Description = $"Embedded file: {Path.GetFileName(filePath)}"
                };

                // Add the embedded file to the document's EmbeddedFiles collection
                doc.EmbeddedFiles.Add(embeddedFile);
            }

            // Save the resulting portfolio PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Portfolio PDF created: {outputPdfPath}");
    }
}
