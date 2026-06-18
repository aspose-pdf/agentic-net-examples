using System;
using System.IO;
using System.Drawing.Imaging;               // ImageFormat for specifying output format
using Aspose.Pdf.Facades;                  // Facade classes for PDF operations

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "encrypted.pdf";          // Encrypted PDF file
        const string outputFolder   = "ExtractedImages";        // Folder to store extracted images
        const string userPassword   = "user123";                // User password for the PDF

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // PdfExtractor implements IDisposable – use using for deterministic cleanup
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Supply the password before binding the PDF
                extractor.Password = userPassword;

                // Bind the encrypted PDF file
                extractor.BindPdf(inputPdfPath);

                // Perform image extraction
                extractor.ExtractImage();

                int imageIndex = 1;
                // Iterate through all extracted images
                while (extractor.HasNextImage())
                {
                    string imagePath = Path.Combine(outputFolder, $"image-{imageIndex}.png");

                    // Save each image as PNG (you can choose other formats via ImageFormat)
                    extractor.GetNextImage(imagePath, ImageFormat.Png);

                    imageIndex++;
                }
            }

            Console.WriteLine($"Image extraction completed. Files saved to '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}