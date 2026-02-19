using System;
using System.IO;
using Aspose.Pdf;

class PdfToHtmlConverter
{
    static void Main(string[] args)
    {
        // Input PDF path (change as needed)
        const string inputPdfPath = "input.pdf";
        // Output HTML path (change as needed)
        const string outputHtmlPath = "output.html";

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

            // Save the document as HTML using default options.
            // The .html extension tells Aspose.Pdf to use HtmlSaveOptions internally.
            pdfDocument.Save(outputHtmlPath);

            Console.WriteLine($"Conversion completed successfully. HTML saved to '{outputHtmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}