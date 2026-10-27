using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToJpegConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "JpegPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Use the CropBox of the first page for precise cropping (apply to all pages)
            Aspose.Pdf.Rectangle cropBox = pdfDoc.Pages[1].CropBox;
            foreach (Page page in pdfDoc.Pages)
            {
                page.CropBox = cropBox;
            }

            // Create a JPEG device with 300 DPI resolution and maximum quality (100)
            JpegDevice jpegDevice = new JpegDevice(new Resolution(300), 100)
            {
                // Render using the CropBox coordinates
                CoordinateType = PageCoordinateType.CropBox
            };

            // Convert each page to a JPEG image
            foreach (Page page in pdfDoc.Pages)
            {
                string outputPath = Path.Combine(outputFolder, $"Page_{page.Number}.jpg");
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    // Process overload takes (Page, Stream)
                    jpegDevice.Process(page, imageStream);
                }
                Console.WriteLine($"Saved page {page.Number} as JPEG -> {outputPath}");
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}
