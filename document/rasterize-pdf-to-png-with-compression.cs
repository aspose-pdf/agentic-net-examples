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
        const string outputDir = "output_images";

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
            // ------------------------------------------------------------
            // Configure PNG compression via ImageCompressionOptions
            // ------------------------------------------------------------
            OptimizationOptions opt = new OptimizationOptions();

            // Enable image compression inside the PDF
            opt.ImageCompressionOptions.CompressImages = true;

            // Set compression quality (0‑100). Higher values give stronger compression.
            // This influences the PNG compression level when the pages are rasterized.
            opt.ImageCompressionOptions.ImageQuality = 80; // example value

            // Apply the optimization settings to the document
            pdfDoc.OptimizeResources(opt);

            // ------------------------------------------------------------
            // Rasterize each page to a lossless PNG image
            // ------------------------------------------------------------
            // Define the resolution for the PNG output (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.png");

                // Save each page as a PNG file
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }
            }
        }

        Console.WriteLine("Rasterization completed.");
    }
}