using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToJpegConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputFolder = "JpegImages";        // folder for JPEG files

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Define the resolution (150 DPI) and JPEG quality (90)
        var resolution = new Resolution(150);
        const int jpegQuality = 90;

        // Create a single JpegDevice instance (resolution + quality)
        var jpegDevice = new JpegDevice(resolution, jpegQuality);

        // Iterate through all pages and convert each to a JPEG image
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string outputFile = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");
            // Render the page into a JPEG file via a FileStream
            using (FileStream outStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                jpegDevice.Process(pdfDocument.Pages[pageNumber], outStream);
            }
        }

        Console.WriteLine("PDF has been converted to JPEG images (150 DPI, quality 90).");
    }
}
