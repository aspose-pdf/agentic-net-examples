using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputDir = "Images";            // folder for JPEGs

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Error: File '{inputPdf}' not found.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Set desired resolution (150 DPI) and JPEG quality (85)
            var resolution = new Resolution(150);
            const int jpegQuality = 85;

            // JpegDevice does not implement IDisposable and does not expose a Compression property in recent versions.
            // It is instantiated once and used to render each page to a FileStream.
            var jpegDevice = new JpegDevice(resolution, jpegQuality);

            // Iterate through all pages and save each as a JPEG image
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");

                // Render the page into a file stream
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNumber], outStream);
                }
            }
        }

        Console.WriteLine("PDF pages have been converted to JPEG images at 150 DPI.");
    }
}
