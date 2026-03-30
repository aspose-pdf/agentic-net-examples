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
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPath);

        // Restrict extraction to the first page only
        extractor.StartPage = 1;
        extractor.EndPage = 1;

        // Extract images (default extracts all images on the page)
        extractor.ExtractImage();

        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bool success = extractor.GetNextImage(memoryStream);
                if (success)
                {
                    byte[] imageBytes = memoryStream.ToArray();
                    Console.WriteLine($"Image {imageIndex}: {imageBytes.Length} bytes extracted.");
                    // imageBytes now contains the raw image data for further analysis
                }
                else
                {
                    Console.WriteLine($"Failed to extract image {imageIndex}.");
                }
            }
            imageIndex++;
        }

        extractor.Close();
    }
}
