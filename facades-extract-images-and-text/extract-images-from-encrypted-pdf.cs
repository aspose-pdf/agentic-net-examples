using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "encrypted.pdf";
        const string outputDir = "ExtractedImages";
        const string userPassword = "user123";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor facade to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Supply the user password for the encrypted PDF
            extractor.Password = userPassword;

            // Bind the encrypted PDF file
            extractor.BindPdf(inputPdf);

            // Perform the image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            // Retrieve each extracted image and save it to a file
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(outputDir, $"image-{imageIndex}.jpg");
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }
        }

        Console.WriteLine($"Image extraction completed. Files saved to '{outputDir}'.");
    }
}