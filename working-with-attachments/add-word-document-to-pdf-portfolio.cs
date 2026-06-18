using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string wordPath   = "sample.docx";      // Word document to embed
        const string outputPath = "portfolio.pdf";    // Resulting PDF portfolio

        if (!File.Exists(wordPath))
        {
            Console.Error.WriteLine($"Word file not found: {wordPath}");
            return;
        }

        // Create a new PDF document (empty portfolio)
        using (Document pdfDoc = new Document())
        {
            // Adding a blank page avoids edge‑case issues
            pdfDoc.Pages.Add();

            // Create a FileSpecification for the Word document using the recommended pattern
            var fileSpec = new FileSpecification(Path.GetFileName(wordPath), "Embedded Word document");
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(wordPath));

            // Add the file to the PDF's embedded files collection (portfolio)
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the PDF portfolio
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF portfolio created at '{outputPath}'.");
    }
}
