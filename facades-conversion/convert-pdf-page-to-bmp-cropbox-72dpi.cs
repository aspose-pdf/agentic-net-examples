using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "page5.bmp";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Ensure the requested page exists
        const int pageNumber = 5;
        if (pdfDocument.Pages.Count < pageNumber)
        {
            Console.Error.WriteLine($"PDF does not contain page {pageNumber}.");
            return;
        }

        // Create a BMP device with 72 DPI resolution
        BmpDevice bmpDevice = new BmpDevice(new Resolution(72));

        // Use the CropBox coordinates for the conversion
        bmpDevice.CoordinateType = PageCoordinateType.CropBox;

        // Render page 5 to a BMP image and save it
        using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            bmpDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
        }

        Console.WriteLine($"Page {pageNumber} has been saved as BMP to '{outputPath}'.");
    }
}
