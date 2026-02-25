using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "optimized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Enable resource merging when pages are added (optional, helps size)
            pdfDoc.OptimizeSize = true;

            // Create an optimization strategy with desired options
            OptimizationOptions optOptions = OptimizationOptions.All();
            optOptions.CompressObjects      = true;   // compress object streams
            optOptions.RemoveUnusedObjects  = true;   // delete objects not referenced
            optOptions.RemoveUnusedStreams  = true;   // delete unused resource streams
            optOptions.SubsetFonts          = true;   // embed only used glyphs
            optOptions.LinkDuplicateStreams = true;   // reuse identical streams
            // You can also set image compression if needed:
            // optOptions.ImageCompressionOptions = new ImageCompressionOptions { ImageQuality = 75 };

            // Apply the optimization to the document
            pdfDoc.OptimizeResources(optOptions);

            // Linearize the PDF for faster web access (optional)
            pdfDoc.Optimize();

            // Save the optimized PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}