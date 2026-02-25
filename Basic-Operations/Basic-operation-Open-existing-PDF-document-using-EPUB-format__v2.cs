using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.epub";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the existing PDF document
            using (Document doc = new Document(inputPath))
            {
                // Set EPUB save options – use flow content recognition mode
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };

                // Save the PDF as an EPUB file
                doc.Save(outputPath, epubOptions);
            }

            Console.WriteLine($"PDF successfully converted to EPUB: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}