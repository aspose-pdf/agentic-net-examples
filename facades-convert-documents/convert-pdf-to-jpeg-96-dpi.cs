using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create a Resolution object with 96 DPI (web‑friendly)
            Resolution resolution = new Resolution(96);

            // Initialize the JPEG device with the specified resolution
            JpegDevice jpegDevice = new JpegDevice(resolution);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");

                // Create a file stream for the output image
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Convert the current page to JPEG and write to the stream
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }
            }
        }

        Console.WriteLine("PDF pages have been successfully converted to JPEG images at 96 DPI.");
    }
}