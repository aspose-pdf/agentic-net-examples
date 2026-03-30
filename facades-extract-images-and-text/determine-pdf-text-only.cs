using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the extractor and bind the PDF file
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);
            // Extract images from the document (resources or actually used)
            extractor.ExtractImage();

            int imageCount = 0;
            while (extractor.HasNextImage())
            {
                imageCount++;
                string imageFileName = $"image-{imageCount}.png";
                // Save the next image to a file; format defaults to PNG
                extractor.GetNextImage(imageFileName);
            }

            bool isTextOnly = imageCount == 0;
            if (isTextOnly)
            {
                Console.WriteLine("The PDF is text‑only (no images extracted).");
            }
            else
            {
                Console.WriteLine($"The PDF contains {imageCount} image(s). Extracted files are named image-#.png.");
            }
        }
    }
}