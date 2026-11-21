using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_embed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Disable font embedding by unembedding fonts during optimization
            var opt = new OptimizationOptions { UnembedFonts = true };
            doc.OptimizeResources(opt);

            // Save the optimized document (fonts will not be embedded)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved without embedded fonts to '{outputPath}'.");
    }
}
