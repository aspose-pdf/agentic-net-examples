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

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // 1. Optimize resources: remove unused objects, merge duplicates, etc.
            pdfDoc.OptimizeResources();

            // 2. Linearize the document for faster web viewing
            pdfDoc.Optimize();

            // 3. Enable the internal size‑optimization flag (merges equal resource streams)
            pdfDoc.OptimizeSize = true;

            // Optional: apply a more aggressive optimization strategy
            // var options = OptimizationOptions.All();
            // pdfDoc.OptimizeResources(options);

            // Save the optimized PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}