using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where extracted images will be saved
        const string outputDir = "ExtractedImages";

        // Desired image format: "png" or "jpeg"
        const string imageFormat = "png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Choose resolution for the exported images
            Resolution resolution = new Resolution(300);

            // Create the appropriate image device based on the requested format
            ImageDevice device;
            if (string.Equals(imageFormat, "jpeg", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(imageFormat, "jpg", StringComparison.OrdinalIgnoreCase))
            {
                device = new JpegDevice(resolution);
            }
            else // default to PNG
            {
                device = new PngDevice(resolution);
            }

            // Iterate through all pages (page indexing is 1‑based)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                Page page = pdfDocument.Pages[pageNumber];

                // Build output file name: image1.png, image2.png, ...
                string outputPath = Path.Combine(
                    outputDir,
                    $"page_{pageNumber}_image.{(device is JpegDevice ? "jpeg" : "png")}");

                // Export the page as an image (lifecycle rule: use ImageDevice.Process)
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    device.Process(page, imageStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as image: {outputPath}");
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}