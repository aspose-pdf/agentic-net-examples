using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF and save it as a new PDF (basic copy/convert)
        using (Document doc = new Document(inputPath))
        {
            doc.Save(outputPath); // Save as PDF; no SaveOptions needed for PDF format
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}