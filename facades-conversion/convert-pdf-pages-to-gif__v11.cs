using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "GifPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document – required for the device‑based conversion
        Document pdfDocument = new Document(inputPath);

        // ------------------------------------------------------------
        // PdfConverter usage (as required by the task)
        // ------------------------------------------------------------
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the already loaded document (or you could bind the file path directly)
            converter.BindPdf(pdfDocument);

            // Define the page range we are interested in (1‑5 or less if the PDF is shorter)
            converter.StartPage = 1;
            converter.EndPage   = Math.Min(5, pdfDocument.Pages.Count);

            // Set the resolution – 72 DPI as requested
            converter.Resolution = new Resolution(72);

            // Perform the internal conversion step.  This call is mandatory before any
            // SaveAs* methods are used, even though we will later use a device class for GIF output.
            converter.DoConvert();
        }

        // ------------------------------------------------------------
        // Convert each selected page to a GIF image using GifDevice.
        // GifDevice works with the CropBox by default, so no extra
        // ImageSaveOptions are needed.
        // ------------------------------------------------------------
        int lastPage = Math.Min(5, pdfDocument.Pages.Count);
        for (int pageNumber = 1; pageNumber <= lastPage; pageNumber++)
        {
            string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.gif");

            using (FileStream outStream = new FileStream(outPath, FileMode.Create))
            {
                // The device respects the resolution set in its constructor.
                GifDevice gifDevice = new GifDevice(new Resolution(72));
                gifDevice.Process(pdfDocument.Pages[pageNumber], outStream);
            }

            Console.WriteLine($"Saved page {pageNumber} as GIF to {outPath}");
        }
    }
}
