using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_unembedded.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (Document doc = new Document(fs))
        {
            // Disable font embedding by unembedding fonts during optimization
            OptimizationOptions opt = new OptimizationOptions
            {
                UnembedFonts = true
            };
            doc.OptimizeResources(opt);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF without embedded fonts to '{outputPath}'.");
    }
}