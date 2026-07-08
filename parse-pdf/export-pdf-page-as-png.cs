using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputImage = "page1.png";
        const int dpi = 300; // Desired resolution in dots per inch

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Verify that the document contains at least one page
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Select the page to rasterize (Aspose.Pdf uses 1‑based indexing)
            Page page = pdfDoc.Pages[1];

            // Create a Resolution object specifying the desired DPI
            Resolution resolution = new Resolution(dpi);

            // PngDevice does NOT implement IDisposable, so instantiate it without a using block
            PngDevice pngDevice = new PngDevice(resolution);

            // Dispose only the stream; the device does not need explicit disposal
            using (FileStream outStream = new FileStream(outputImage, FileMode.Create, FileAccess.Write))
            {
                pngDevice.Process(page, outStream);
            }

            Console.WriteLine($"Page rasterized to '{outputImage}' at {dpi} DPI.");
        }
    }
}
