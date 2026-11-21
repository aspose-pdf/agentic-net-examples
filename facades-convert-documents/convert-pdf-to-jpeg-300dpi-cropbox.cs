using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        Document pdfDoc = new Document(inputPdfPath);

        // Prepare a JPEG device with 300 DPI resolution and 90% quality.
        // In Aspose.PDF 26.9+ the JpegDevice uses the page's CropBox by default,
        // so the explicit UseCropBox property is no longer required (and has been removed).
        var jpegDevice = new JpegDevice(new Resolution(300), 90);

        // Convert each page to a separate JPEG image
        for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
        {
            string outputImagePath = Path.Combine(outputFolder, $"Page_{pageNumber}.jpeg");
            using (var fileStream = new FileStream(outputImagePath, FileMode.Create, FileAccess.Write))
            {
                jpegDevice.Process(pdfDoc.Pages[pageNumber], fileStream);
            }
            Console.WriteLine($"Page {pageNumber} saved as {outputImagePath}");
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}
