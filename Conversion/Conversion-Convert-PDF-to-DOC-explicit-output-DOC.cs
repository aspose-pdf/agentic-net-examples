using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Paths for source PDF and target DOC
        const string inputPdfPath = "input.pdf";
        const string outputDocPath = "output.doc";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Configure DOC save options (binary .doc format)
        DocSaveOptions docOptions = new DocSaveOptions
        {
            Format = DocSaveOptions.DocFormat.Doc   // Use DocFormat.DocX for .docx
        };

        // Save the document as DOC
        pdfDocument.Save(outputDocPath, docOptions);

        Console.WriteLine($"Conversion completed successfully. DOC saved to '{outputDocPath}'.");
    }
}