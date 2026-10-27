using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.ps";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PostScript file using the core Document constructor (auto‑detects format)
            using (Document doc = new Document(inputPath))
            {
                // Save the document as PDF using default settings
                doc.Save(outputPath);
            }

            Console.WriteLine($"PostScript successfully converted to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
