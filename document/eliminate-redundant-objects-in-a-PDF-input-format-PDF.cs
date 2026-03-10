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

        try
        {
            // Load the PDF document
            using (Document pdf = new Document(inputPath))
            {
                // Remove unused resources, merge duplicate objects, etc.
                pdf.OptimizeResources();

                // If you want to enable all non‑destructive options:
                // OptimizationOptions opts = OptimizationOptions.All();
                // pdf.OptimizeResources(opts);

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