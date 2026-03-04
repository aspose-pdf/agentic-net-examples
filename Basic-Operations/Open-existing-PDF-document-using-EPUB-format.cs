using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputEpub = "output.epub";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialize EPUB save options (optional: set content recognition mode)
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };

                // Save the PDF as an EPUB file
                pdfDoc.Save(outputEpub, epubOptions);
            }

            Console.WriteLine($"PDF successfully converted to EPUB: '{outputEpub}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}