using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace contains Document, DocSaveOptions, etc.

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Set custom metadata properties
            pdfDocument.Info.Title = "Custom Document Title";
            pdfDocument.Info.Author = "John Doe";

            // Configure DOCX save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Export as DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Use Flow mode for better editability (optional)
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Enable bullet recognition (optional)
                RecognizeBullets = true
            };

            // Save the document as DOCX (lifecycle rule: use Document.Save with SaveOptions)
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX with metadata at '{outputDocxPath}'.");
    }
}
