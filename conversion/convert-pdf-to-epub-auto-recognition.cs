using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.epub";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document; using ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Set EPUB save options with automatic content recognition
            EpubSaveOptions epubOptions = new EpubSaveOptions
            {
                ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
            };

            // Save the document as EPUB using the specified options
            doc.Save(outputPath, epubOptions);
        }

        Console.WriteLine($"PDF successfully converted to EPUB: {outputPath}");
    }
}