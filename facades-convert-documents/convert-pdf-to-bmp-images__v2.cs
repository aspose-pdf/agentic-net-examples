using System;
using System.IO;
using Aspose.Pdf;                     // Document, etc.
using Aspose.Pdf.Devices;            // BmpDevice for rasterization

class PdfToBmpConverter
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output directory for BMP images
        const string outputDir = "BmpImages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(pdfPath);

        // Create a BmpDevice once (it does not implement IDisposable, so no using block)
        BmpDevice bmpDevice = new BmpDevice(200, 200);

        // Iterate through each page and rasterize it to a BMP image at 200 DPI
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string bmpPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");

            // Use a using block only for the FileStream (which *does* implement IDisposable)
            using (FileStream imageStream = new FileStream(bmpPath, FileMode.Create, FileAccess.Write))
            {
                // Render the current page into the BMP stream
                bmpDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }
        }

        Console.WriteLine("PDF has been converted to BMP images successfully.");
    }
}
