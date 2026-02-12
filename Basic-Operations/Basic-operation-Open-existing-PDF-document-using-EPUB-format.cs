using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputEpubPath = "output.epub";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure EPUB save options (optional settings)
            EpubSaveOptions epubOptions = new EpubSaveOptions();
            epubOptions.Title = "Converted EPUB";
            // The RecognitionMode property is optional; if needed, use the correct enum name.
            // epubOptions.RecognitionMode = EpubSaveOptions.RecognitionMode.PdfFlow; // Uncomment and adjust if required.

            // Save the PDF as an EPUB file using the configured options
            pdfDocument.Save(outputEpubPath, epubOptions);

            Console.WriteLine($"PDF successfully converted to EPUB and saved as '{outputEpubPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
