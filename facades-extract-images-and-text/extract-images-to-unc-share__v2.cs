using System;
using System.IO;
using System.Drawing.Imaging; // ImageFormat for specifying output image type
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = @"C:\Docs\sample.pdf";

        // UNC folder where extracted images will be saved
        const string uncFolder = @"\\Server\Share\Images";

        // Ensure the UNC directory exists
        if (!Directory.Exists(uncFolder))
        {
            Console.Error.WriteLine($"UNC folder not found: {uncFolder}");
            return;
        }

        // Validate source PDF existence
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Extract images using Aspose.Pdf.Facades.PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(pdfPath);

            // Prepare the extractor to retrieve images
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop through all images in the PDF
            while (extractor.HasNextImage())
            {
                // Build the UNC file name for the current image
                string outputFile = Path.Combine(uncFolder, $"image-{imageIndex}.png");

                // Save the image as PNG (you can change ImageFormat as needed)
                bool success = extractor.GetNextImage(outputFile, ImageFormat.Png);

                if (!success)
                {
                    Console.Error.WriteLine($"Failed to extract image #{imageIndex}");
                }

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}