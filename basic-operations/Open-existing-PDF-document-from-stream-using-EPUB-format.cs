using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputEpubPath = "output.epub";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Open the PDF from a file stream
            using (FileStream pdfStream = File.OpenRead(inputPdfPath))
            using (Document doc = new Document(pdfStream))
            {
                // Configure EPUB save options
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    // Optional: choose a recognition mode for content flow
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };

                // Save the document as EPUB using the save options
                doc.Save(outputEpubPath, epubOptions);
            }

            Console.WriteLine($"PDF opened from stream and saved as EPUB: '{outputEpubPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}