using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string userPassword = "user123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Supply the user password for the encrypted PDF
            extractor.Password = userPassword;
            // Bind the encrypted PDF file
            extractor.BindPdf(inputPath);

            // Extract images from the document
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string outputFile = "image-" + imageIndex + ".png";
                extractor.GetNextImage(outputFile);
                Console.WriteLine($"Saved image {imageIndex} to {outputFile}");
                imageIndex++;
            }
        }
    }
}