using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_compressed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Create an optimization strategy (all default options enabled)
            OptimizationOptions opt = OptimizationOptions.All();

            // Access the image compression options and configure them
            ImageCompressionOptions imgOpts = opt.ImageCompressionOptions;
            imgOpts.CompressImages   = true;   // enable image compression
            imgOpts.ImageQuality     = 50;    // 0‑100, lower = higher compression
            imgOpts.ResizeImages    = true;   // allow resizing of high‑resolution images
            imgOpts.MaxResolution   = 150;   // maximum DPI; images above will be downscaled
            // The Version property expects an ImageCompressionVersion enum, not an int.
            imgOpts.Version = ImageCompressionVersion.Fast; // fast algorithm (1=Standard, 2=Fast, 3=Mixed)

            // Apply the optimization to the document
            doc.OptimizeResources(opt);

            // Save the optimized PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}
