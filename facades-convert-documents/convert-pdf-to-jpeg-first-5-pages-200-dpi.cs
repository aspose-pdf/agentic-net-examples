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

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document with deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Set the desired resolution (200 DPI)
            Resolution resolution = new Resolution(200);

            // Initialize the JPEG device with the resolution
            JpegDevice jpegDevice = new JpegDevice(resolution);

            // Convert only the first five pages (or fewer if the document has less)
            int pageCount = Math.Min(5, pdfDocument.Pages.Count);
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpeg");

                // Write each page to a JPEG file
                using (FileStream jpegStream = new FileStream(outputPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as JPEG to '{outputPath}'.");
            }
        }
    }
}
