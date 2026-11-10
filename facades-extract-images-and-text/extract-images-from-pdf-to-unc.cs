using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // UNC path to the network‑mapped folder where images will be saved
        // Example: \\fileserver\shared\pdf_images
        const string uncFolder = @"\\fileserver\shared\pdf_images";

        // Validate source PDF existence
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the target UNC directory exists
        Directory.CreateDirectory(uncFolder);

        // PdfExtractor is a Facade class that can extract images, text, etc.
        // It implements IDisposable, so wrap it in a using block.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(pdfPath);

            // Configure the extractor to extract images only
            extractor.ExtractImage();

            int imageIndex = 1;

            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Build a file name for each image (PNG format)
                string imageFile = Path.Combine(uncFolder, $"Image_{imageIndex}.png");

                // Save the current image to the UNC path
                extractor.GetNextImage(imageFile);

                imageIndex++;
            }

            Console.WriteLine($"Extracted {imageIndex - 1} image(s) to \"{uncFolder}\".");
        }
    }
}