using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const int startPage = 1;
        const int endPage = 3; // inclusive preview range
        const int resolutionDpi = 150; // desired image resolution
        const int jpegQuality = 90; // JPEG quality (0‑100)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(inputPdf);

        // Validate the requested page range against the actual page count.
        int maxPage = Math.Min(endPage, pdfDocument.Pages.Count);
        if (startPage < 1 || startPage > maxPage)
        {
            Console.Error.WriteLine("Invalid start page specified.");
            return;
        }

        // Configure a JPEG device – this is the cross‑platform way to render pages as JPEG images.
        // Resolution controls the DPI of the output image; ImageQuality controls JPEG compression.
        JpegDevice jpegDevice = new JpegDevice(new Resolution(resolutionDpi), jpegQuality);

        for (int pageNumber = startPage; pageNumber <= maxPage; pageNumber++)
        {
            string outputImagePath = $"page_{pageNumber}.jpg";
            using (FileStream imageStream = new FileStream(outputImagePath, FileMode.Create, FileAccess.Write))
            {
                // Render the specific page to the JPEG stream.
                // JpegDevice.Process expects a Page object and an output stream (2 arguments).
                Page page = pdfDocument.Pages[pageNumber];
                jpegDevice.Process(page, imageStream);
            }
            Console.WriteLine($"Saved page {pageNumber} as {outputImagePath}");
        }
    }
}
