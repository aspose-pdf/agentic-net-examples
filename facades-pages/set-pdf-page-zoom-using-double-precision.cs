using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputImagePath = "page1_zoom.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(inputPdfPath);

        // Desired zoom factor (e.g., 2.5 = 250%).
        const float zoomFactor = 2.5f;
        // Convert zoom factor to DPI (default PDF DPI is 72).
        int dpi = (int)(72 * zoomFactor);

        // Render the first page to a PNG image with the calculated DPI.
        using (FileStream imageStream = new FileStream(outputImagePath, FileMode.Create, FileAccess.Write))
        {
            // PngDevice respects the supplied Resolution (DPI) which effectively applies the zoom.
            PngDevice pngDevice = new PngDevice(new Resolution(dpi));
            pngDevice.Process(pdfDocument.Pages[1], imageStream);
        }

        Console.WriteLine($"Page rendered with {zoomFactor * 100}% zoom saved to '{outputImagePath}'.");
    }
}
