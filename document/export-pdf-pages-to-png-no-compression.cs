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
        const string outputDir = "PageImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDoc = new Document(inputPdf);

        // Disable image compression using the modern OptimizationOptions API
        OptimizationOptions opt = OptimizationOptions.All();
        opt.CompressImages = false; // keep original image data
        pdfDoc.OptimizeResources(opt);

        // Create a PNG device (150 DPI by default) – PNG is loss‑less
        var pngDevice = new PngDevice(new Resolution(150));

        // Iterate through all pages (1‑based indexing)
        for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
        {
            string outPath = Path.Combine(outputDir, $"page_{pageNum}.png");

            // Convert the page to PNG and save to file
            using (FileStream outStream = new FileStream(outPath, FileMode.Create))
            {
                pngDevice.Process(pdfDoc.Pages[pageNum], outStream);
            }

            Console.WriteLine($"Saved page {pageNum} as PNG → {outPath}");
        }

        Console.WriteLine("All pages have been exported without image compression.");
    }
}