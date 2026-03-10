using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;   // contains OptimizationOptions

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

        try
        {
            // Load the PDF document
            using (Document pdf = new Document(inputPath))
            {
                // Create an optimization strategy with all non‑functional options enabled
                OptimizationOptions opt = OptimizationOptions.All();

                // Example: limit image resolution to 150 DPI (reduces size for high‑res images)
                opt.MaxResoultion = 150;   // note: property name is "MaxResoultion" as defined in the API

                // Example: set image encoding to JPEG (applies compression)
                opt.ImageEncoding = ImageEncoding.Jpeg;

                // Apply the optimization to the document
                pdf.OptimizeResources(opt);

                // Save the optimized PDF
                pdf.Save(outputPath);
            }

            Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}