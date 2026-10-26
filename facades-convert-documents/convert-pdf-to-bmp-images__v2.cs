using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // BmpDevice, Resolution, PageCoordinateType

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Base output path for BMP images (page number will be appended automatically)
        const string outputBasePath = "output/page"; // e.g., "output/page" -> page1.bmp, page2.bmp, ...

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists (handle case where Path.GetDirectoryName returns null)
        string outputDir = Path.GetDirectoryName(outputBasePath);
        if (string.IsNullOrEmpty(outputDir))
            outputDir = "."; // current directory
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Configure the desired resolution (DPI) for the BMP images
        Resolution resolution = new Resolution(300); // 300 DPI

        // BmpDevice does NOT implement IDisposable – instantiate it directly.
        // Use the constructor that accepts a Resolution (width/height are optional and default to page size).
        BmpDevice bmpDevice = new BmpDevice(resolution);
        // Optional: set the coordinate system you need (CropBox, MediaBox, etc.)
        // bmpDevice.CoordinateType = PageCoordinateType.CropBox;

        // Iterate through each page and convert it to a BMP image using BmpDevice
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string outputPath = $"{outputBasePath}{pageNumber}.bmp";

            // Create a file stream for the output BMP. The stream is disposable, so we use a using block.
            using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Convert the current page to BMP. The Process method takes (Page, Stream).
                bmpDevice.Process(pdfDocument.Pages[pageNumber], outStream);
            }
        }

        Console.WriteLine("PDF has been converted to BMP images.");
    }
}
