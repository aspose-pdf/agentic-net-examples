using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // JpegDevice, Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Set the desired resolution (96 DPI) and JPEG quality (100 is max)
            Resolution resolution = new Resolution(96);
            JpegDevice jpegDevice = new JpegDevice(resolution, 100);

            // Iterate through each page and save it as a JPEG image
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");
                // The Process method can write directly to a file path
                jpegDevice.Process(pdfDocument.Pages[pageNumber], outputPath);
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}
