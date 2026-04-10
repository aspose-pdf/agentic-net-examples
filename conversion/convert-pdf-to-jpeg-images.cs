using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputDir = "Images";             // folder for JPEG files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // JpegDevice with default resolution and maximum quality
            JpegDevice jpegDevice = new JpegDevice();

            // Iterate over all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.jpg");

                // Create the output stream inside a using block
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    // Convert the current page to JPEG and write to the stream
                    jpegDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }

                Console.WriteLine($"Saved page {pageNum} as JPEG → {outPath}");
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}