using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "OutputImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Prepare JPEG device with 150 DPI resolution (horizontal & vertical)
        // Using the two‑parameter constructor avoids ambiguous overloads.
        JpegDevice jpegDevice = new JpegDevice(new Resolution(150, 150));

        // Convert pages 1 through 10 (or up to the last page if the PDF has fewer pages)
        int lastPage = Math.Min(10, pdfDocument.Pages.Count);
        for (int pageNumber = 1; pageNumber <= lastPage; pageNumber++)
        {
            // Ensure the page's CropBox is used – Aspose.Pdf renders using CropBox by default.
            // If you need to modify the CropBox, you can do it here, e.g.:
            // pdfDocument.Pages[pageNumber].CropBox = new Aspose.Pdf.Rectangle(...);

            string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");
            try
            {
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create))
                {
                    // Render the specific page to JPEG
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], outStream);
                }
            }
            catch (Exception ex)
            {
                // If Aspose.Pdf tries to start an external helper process and fails,
                // provide a clear message. The most common cause is a missing or
                // mismatched Aspose.Pdf assembly. Ensure the NuGet package is added
                // and the correct version is copied to the output folder.
                Console.Error.WriteLine($"Failed to convert page {pageNumber}: {ex.Message}");
            }
        }

        Console.WriteLine("Conversion of pages 1‑10 to JPEG completed.");
    }
}
