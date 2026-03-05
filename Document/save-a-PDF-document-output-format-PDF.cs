using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the destination PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block to ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Any processing or modifications to the document would go here

            // Save the document as a PDF file (output format is PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
    }
}