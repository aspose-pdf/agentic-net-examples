using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Disable image compression during optimization to keep original image data unchanged
            OptimizationOptions opt = new OptimizationOptions();
            opt.CompressImages = false; // do not compress images
            // Apply the optimization options (no other changes are required)
            pdfDoc.OptimizeResources(opt);

            // Create a JPEG device with maximum quality (100) and default resolution (150 DPI)
            JpegDevice jpegDevice = new JpegDevice(quality: 100);

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.jpg");

                // Convert the current page to JPEG and save to file
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }

                Console.WriteLine($"Saved page {pageNum} as JPEG → {outPath}");
            }
        }
    }
}
