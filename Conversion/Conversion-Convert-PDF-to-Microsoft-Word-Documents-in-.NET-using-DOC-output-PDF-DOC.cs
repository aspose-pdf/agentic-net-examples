using System;
using System.IO;
using Aspose.Pdf;

class PdfToDocConverter
{
    static void Main(string[] args)
    {
        // Input PDF path (change as needed)
        const string inputPdfPath = "input.pdf";
        // Output DOC path (the .doc extension tells Aspose.Pdf to save in Word format)
        const string outputDocPath = "output.doc";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Save the document as a Word .doc file
            // This uses the built‑in save overload that infers format from the file extension
            pdfDocument.Save(outputDocPath);

            Console.WriteLine($"Conversion successful. DOC file saved to '{outputDocPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}