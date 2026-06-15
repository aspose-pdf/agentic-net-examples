using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // for JpegDevice

class PdfToJpegConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output folder for JPEG images
        const string outputFolder = "JpegImages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // JpegDevice allows us to specify the resolution (DPI) directly.
            // 300 DPI is required.
            var jpegDevice = new JpegDevice(300);

            // Iterate through each page and save it as a JPEG image.
            int pageIndex = 1;
            foreach (Page page in pdfDocument.Pages)
            {
                string outputFile = Path.Combine(outputFolder, $"page_{pageIndex}.jpeg");

                // The JpegDevice processes the page using the CropBox by default,
                // which provides the precise cropping required.
                jpegDevice.Process(page, outputFile);
                pageIndex++;
            }
        }

        Console.WriteLine("PDF conversion to JPEG completed.");
    }
}
