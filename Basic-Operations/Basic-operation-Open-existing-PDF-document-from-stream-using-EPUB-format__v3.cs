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
            using (Document pdfDoc = new Document(pdfStream))
            {
                // Configure EPUB save options (use flow recognition mode)
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow,
                    Title = "Converted EPUB"
                };

                // Save the document as EPUB
                pdfDoc.Save(outputEpubPath, epubOptions);
            }

            Console.WriteLine($"PDF successfully converted to EPUB: {outputEpubPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}