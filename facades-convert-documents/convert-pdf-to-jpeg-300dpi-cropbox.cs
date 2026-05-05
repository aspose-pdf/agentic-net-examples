using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output folder for JPEG images
        const string outputFolder = "output_images";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle: using block for deterministic disposal)
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create a Resolution object with 300 DPI
            Resolution resolution = new Resolution(300);

            // Initialize JpegDevice with the specified resolution
            JpegDevice jpegDevice = new JpegDevice(resolution);

            // Set coordinate type to CropBox for precise cropping
            jpegDevice.CoordinateType = PageCoordinateType.CropBox;

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build output file name for each page
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpeg");

                // Convert the current page to JPEG and save to file
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }
            }
        }

        Console.WriteLine("PDF successfully converted to JPEG images.");
    }
}