using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "OutputImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // JpegDevice does NOT implement IDisposable, so instantiate it directly
            JpegDevice jpegDevice = new JpegDevice(); // default resolution (72 DPI) and default quality

            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.jpg");
                // Convert the current page to a JPEG image
                jpegDevice.Process(pdfDoc.Pages[pageNumber], outPath);
            }
        }

        Console.WriteLine($"PDF pages have been converted to JPEG images in '{outputDir}'.");
    }
}
