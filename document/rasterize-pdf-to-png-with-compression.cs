using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPattern = "page_{0}.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure PNG compression via ImageCompressionOptions
            OptimizationOptions opt = OptimizationOptions.All();
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality = 100; // lossless compression level
            pdfDoc.OptimizeResources(opt);

            // Create a PNG device with a high resolution (300 DPI)
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);
            pngDevice.TransparentBackground = false;

            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = string.Format(outputPattern, pageNum);
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }
                Console.WriteLine($"Saved {outPath}");
            }
        }
    }
}