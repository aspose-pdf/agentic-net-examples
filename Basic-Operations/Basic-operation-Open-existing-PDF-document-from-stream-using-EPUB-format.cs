using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path (replace with your actual file)
        const string pdfFilePath = "input.pdf";
        // Output EPUB file path
        const string epubFilePath = "output.epub";

        // Verify that the input PDF exists
        if (!File.Exists(pdfFilePath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfFilePath}'.");
            return;
        }

        try
        {
            // Open the PDF file as a read‑only stream
            using (FileStream pdfStream = File.OpenRead(pdfFilePath))
            {
                // Load the PDF document from the stream
                Document pdfDocument = new Document(pdfStream);

                // Configure EPUB save options (optional settings)
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    Title = "Converted EPUB"
                    // Additional options such as CacheGlyphs, ContentRecognitionMode can be set here
                };

                // Save the document as EPUB using the specified options
                pdfDocument.Save(epubFilePath, epubOptions);
            }

            Console.WriteLine($"PDF successfully converted to EPUB: '{epubFilePath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}