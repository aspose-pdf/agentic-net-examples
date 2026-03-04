using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputText = "sample.txt";
        const string imagesFolder = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the folder for extracted images exists
        Directory.CreateDirectory(imagesFolder);

        // PdfExtractor implements IDisposable – wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPdf);

            // Optional: set higher resolution for clearer extracted images
            extractor.Resolution = 300; // DPI

            // -------------------------------------------------
            // Extract text
            // -------------------------------------------------
            // 0 = pure text mode (default), 1 = raw ordering mode
            extractor.ExtractTextMode = 0;
            extractor.ExtractText();               // Perform the extraction
            extractor.GetText(outputText);         // Save extracted text to file
            Console.WriteLine($"Text extracted to '{outputText}'.");

            // -------------------------------------------------
            // Extract images
            // -------------------------------------------------
            extractor.ExtractImage();              // Prepare image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(imagesFolder, $"image-{imageIndex}.png");
                // GetNextImage saves the next image; default format is PNG
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Image {imageIndex} saved to '{imagePath}'.");
                imageIndex++;
            }
        }
    }
}