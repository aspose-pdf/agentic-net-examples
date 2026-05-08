using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "encrypted.pdf";      // Encrypted PDF file
        const string outputFolder = "ExtractedImages"; // Folder for extracted images
        const string userPassword = "user123";        // User password for the PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (facade) to handle encrypted PDFs
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Supply the password before binding the PDF
            extractor.Password = userPassword;

            // Bind the encrypted PDF file
            extractor.BindPdf(inputPdf);

            // Perform image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            // Retrieve each extracted image and save it to a file
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(outputFolder, $"image-{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }
        }

        Console.WriteLine($"Image extraction completed. Images saved to '{outputFolder}'.");
    }
}