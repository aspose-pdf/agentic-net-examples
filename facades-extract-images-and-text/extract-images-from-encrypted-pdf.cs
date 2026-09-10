using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input encrypted PDF file and user password
        const string inputPdf   = "encrypted_input.pdf";
        const string userPwd    = "userPassword";
        const string outputDir  = "ExtractedImages";

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create output directory if it does not exist
        Directory.CreateDirectory(outputDir);

        try
        {
            // Create the PdfExtractor facade
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Supply the password required to open the encrypted PDF
                extractor.Password = userPwd;

                // Bind the PDF file to the extractor
                extractor.BindPdf(inputPdf);

                // Perform the image extraction operation
                extractor.ExtractImage();

                // Retrieve each extracted image and save it to the output folder
                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string imagePath = Path.Combine(outputDir, $"image-{imageIndex}.jpg");
                    // GetNextImage(string) saves the image using the default JPEG format
                    extractor.GetNextImage(imagePath);
                    Console.WriteLine($"Saved image {imageIndex} to '{imagePath}'");
                    imageIndex++;
                }

                Console.WriteLine("Image extraction completed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}