using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "optimized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create an OptimizationOptions instance.
            // Example: limit image resolution to 150 DPI and enable compression.
            OptimizationOptions opt = new OptimizationOptions
            {
                // Reduce image resolution if it exceeds this value.
                MaxResoultion = 150,

                // Compress PDF objects (including images) into streams.
                CompressObjects = true,

                // Remove resources that are not used on any page.
                RemoveUnusedObjects = true,
                RemoveUnusedStreams = true
            };

            // Apply the optimization strategy to the document.
            pdfDoc.OptimizeResources(opt);

            // Save the optimized PDF.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}