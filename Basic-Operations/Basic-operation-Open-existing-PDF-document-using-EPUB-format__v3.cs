using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputEpubPath = "output.epub";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure EPUB save options
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    // Optional: set the EPUB title
                    Title = Path.GetFileNameWithoutExtension(outputEpubPath)
                };

                // Save the document as EPUB using the explicit save options
                pdfDoc.Save(outputEpubPath, epubOptions);
            }

            Console.WriteLine($"Successfully converted PDF to EPUB: '{outputEpubPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}