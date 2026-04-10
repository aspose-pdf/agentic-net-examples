using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input encrypted PDF, output directory and user password
        const string inputPdf   = "encrypted_input.pdf";
        const string outputDir  = "ExtractedImages";
        const string userPwd    = "userPassword";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
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
                while (extractor.HasNextImage())
                {
                    // Save each image as a separate file (PNG format by default)
                    string imagePath = Path.Combine(outputDir, $"image-{imageIndex}.png");
                    extractor.GetNextImage(imagePath);
                    Console.WriteLine($"Saved image {imageIndex} → {imagePath}");
                    imageIndex++;
                }

                // No explicit Close() needed – the using block disposes the extractor
            }

            Console.WriteLine("Image extraction completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}