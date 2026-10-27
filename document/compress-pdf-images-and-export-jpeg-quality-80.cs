using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "PageImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf (page-indexing-one-based rule)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                // Configure JPEG device with quality 80
                // Resolution can be adjusted as needed; 150 DPI is a common default
                JpegDevice jpegDevice = new JpegDevice(new Resolution(150), 80);

                string outPath = Path.Combine(outputDir, $"Page_{pageNum}.jpg");

                // Save the individual page as a JPEG image using the device
                using (FileStream imageStream = new FileStream(outPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNum], imageStream);
                }
            }
        }

        Console.WriteLine($"All pages saved as JPEG images in '{outputDir}'.");
    }
}
