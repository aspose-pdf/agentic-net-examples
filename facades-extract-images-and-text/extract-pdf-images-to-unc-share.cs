using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdf = @"C:\Docs\sample.pdf";

        // UNC path to the network share where images will be saved
        // Use a verbatim string to preserve backslashes
        const string uncFolder = @"\\Server\Share\Images";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the destination directory exists (creates it if necessary)
        Directory.CreateDirectory(uncFolder);

        // Use PdfExtractor from Aspose.Pdf.Facades to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(inputPdf);

            // Extract all images from the document
            extractor.ExtractImage();

            int imageIndex = 1;

            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Build a UNC file name for each image (e.g., image-1.jpg, image-2.jpg, ...)
                string outputPath = Path.Combine(uncFolder, $"image-{imageIndex}.jpg");

                // Save the current image to the UNC path (default format is JPEG)
                extractor.GetNextImage(outputPath);

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed successfully.");
    }
}