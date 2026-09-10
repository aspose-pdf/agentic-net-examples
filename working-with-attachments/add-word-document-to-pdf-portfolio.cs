using System;
using System.IO;
using Aspose.Pdf; // Core PDF API

class Program
{
    static void Main()
    {
        const string outputPdfPath = "portfolio.pdf";
        const string wordFilePath = "sample.docx";

        // Verify the Word document exists
        if (!File.Exists(wordFilePath))
        {
            Console.Error.WriteLine($"Word file not found: {wordFilePath}");
            return;
        }

        // Create a new (empty) PDF document
        using (Document pdfDoc = new Document())
        {
            // Initialize the collection that represents a PDF portfolio
            pdfDoc.Collection = new Collection();

            // Create a file specification for the Word document
            var fileSpec = new FileSpecification(wordFilePath, "Word Document");

            // Add the Word document to the portfolio
            pdfDoc.Collection.Add(fileSpec);

            // Save the resulting PDF portfolio
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF portfolio created at '{outputPdfPath}'.");
    }
}
