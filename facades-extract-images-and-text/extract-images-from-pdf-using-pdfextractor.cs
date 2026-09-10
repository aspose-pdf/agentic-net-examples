using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Folder where extracted images will be saved
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use a using block so the PdfExtractor is disposed automatically
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(pdfPath);

            // Extract all images from the document
            extractor.ExtractImage();

            int imageIndex = 1;
            // Retrieve each image while there are more available
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(outputFolder, $"image-{imageIndex}.png");
                // Save the next image to the specified file (default format is JPEG;
                // the file extension can be changed as needed)
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}