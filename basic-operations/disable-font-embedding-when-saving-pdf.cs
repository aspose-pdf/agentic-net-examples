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
        Document doc = new Document(inputPath);

        // Disable font embedding using OptimizationOptions
        var opt = new OptimizationOptions { UnembedFonts = true };
        doc.OptimizeResources(opt);

        // Save the document
        doc.Save(outputPath);

        Console.WriteLine($"PDF saved without embedded fonts to '{outputPath}'.");
    }
}