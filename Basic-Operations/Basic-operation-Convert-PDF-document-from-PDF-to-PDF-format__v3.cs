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

        // Load the source PDF and immediately save it as a new PDF.
        // The Document class implements IDisposable, so we wrap it in a using block.
        using (Document doc = new Document(inputPath))
        {
            // Save creates a copy of the PDF. No SaveOptions are required for PDF‑to‑PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF copied successfully to '{outputPath}'.");
    }
}