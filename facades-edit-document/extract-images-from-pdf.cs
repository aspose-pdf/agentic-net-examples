using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF
        const string pdfPath = "input.pdf";

        // Folder where extracted images will be saved
        const string outputFolder = "ExtractedImages";

        // Verify that the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (a Facade) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(pdfPath);

            // Perform the image extraction operation
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Build a unique file name for each image
                string imagePath = Path.Combine(outputFolder, $"image_{imageIndex}.jpg");

                // Save the current image to the file system
                // GetNextImage returns a bool indicating success; we ignore it here
                extractor.GetNextImage(imagePath);

                imageIndex++;
            }
        }

        Console.WriteLine($"All images have been extracted to '{outputFolder}'.");
    }
}