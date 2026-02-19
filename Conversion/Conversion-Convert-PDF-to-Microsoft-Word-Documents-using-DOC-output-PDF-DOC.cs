using System;
using System.IO;
using Aspose.Pdf;

class PdfToWordConverter
{
    static void Main(string[] args)
    {
        // Input PDF path and output DOC path.
        // Adjust these paths as needed.
        string inputPdfPath = "input.pdf";
        string outputDocPath = "output.doc";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(inputPdfPath);

            // Save the document as a Word .doc file.
            // The format is inferred from the file extension.
            pdfDocument.Save(outputDocPath);
            
            Console.WriteLine($"Conversion successful. DOC saved to '{outputDocPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}