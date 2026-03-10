using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // Path to the source PDF
        const string outputPath = "output.pdf";  // Path for the new PDF

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block to ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Persist the loaded document as a new PDF file
            doc.Save(outputPath); // Save() without SaveOptions writes PDF regardless of extension
        }

        Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
    }
}