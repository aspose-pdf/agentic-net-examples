using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API, includes EpubSaveOptions
// No additional namespaces are required for this operation

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output EPUB file path
        const string epubPath = "output.epub";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Open the existing PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Initialize EPUB save options
                EpubSaveOptions epubOptions = new EpubSaveOptions();

                // Optional: set the EPUB title (if desired)
                epubOptions.Title = Path.GetFileNameWithoutExtension(pdfPath);

                // Optional: choose a content recognition mode (Flow is the default)
                // epubOptions.ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow;

                // Save the PDF as an EPUB file using the explicit save options
                pdfDocument.Save(epubPath, epubOptions);
            }

            Console.WriteLine($"PDF successfully converted to EPUB: '{epubPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during loading or saving
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}