using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDocx = "output.docx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Add custom metadata (author and title)
            pdfDoc.Info.Author = "John Doe";
            pdfDoc.Info.Title  = "Converted Document";

            // Configure DOCX save options (must pass explicit SaveOptions for non‑PDF formats)
            DocSaveOptions docxOptions = new DocSaveOptions
            {
                // Output format DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Use full flow recognition for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Enable bullet recognition (optional)
                RecognizeBullets = true
            };

            // Save the document as DOCX (lifecycle rule: use Document.Save with options)
            pdfDoc.Save(outputDocx, docxOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocx}");
    }
}