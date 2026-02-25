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
            // Open the existing PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Configure EPUB save options with the correct nested enum
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };

                // Save the PDF as an EPUB file
                doc.Save(outputEpub, epubOptions);
            }

            Console.WriteLine($"PDF successfully converted to EPUB: {outputEpub}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}