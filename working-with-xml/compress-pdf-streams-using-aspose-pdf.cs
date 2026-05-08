using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // PDF generated from XML
        const string outputPath = "compressed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create optimization options and enable object compression
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true
                // Other options remain at their defaults
            };

            // Apply the optimization strategy to the document resources
            pdfDoc.OptimizeResources(opt);

            // Save the optimized (compressed) PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPath}'.");
    }
}