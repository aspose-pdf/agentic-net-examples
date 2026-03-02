using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_optimized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPath))
            {
                // Remove unused resources, merge duplicate streams, etc.
                pdfDoc.OptimizeResources();

                // Linearize the document for faster web access.
                pdfDoc.Optimize();

                // Apply a full set of optimization options (e.g., font subsetting, duplicate removal).
                OptimizationOptions opt = OptimizationOptions.All();
                pdfDoc.OptimizeResources(opt);

                // Save the optimized PDF.
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}