using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "encrypted_input.pdf";   // Encrypted PDF file
        const string outputDir  = "ExtractedImages";      // Folder to store images
        const string userPwd    = "userPassword";         // User password for the PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfExtractor implements IDisposable – use a using block for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Supply the password before binding the PDF
            extractor.Password = userPwd;

            // Bind the encrypted PDF file
            extractor.BindPdf(inputPdf);

            // Extract all images from the document
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(outputDir, $"image-{imageIndex}.png");
                // Save each image to a file (default format is PNG when using GetNextImage(string))
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Saved image {imageIndex} to '{imagePath}'");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}