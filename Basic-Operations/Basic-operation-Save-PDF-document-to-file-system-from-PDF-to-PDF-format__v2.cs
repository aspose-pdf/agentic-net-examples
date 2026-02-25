using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF file
        const string outputPath = "output.pdf";  // destination PDF file

        // Verify that the source file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Wrap the Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Save the document back to PDF format.
            // No SaveOptions are required because the target format is PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
    }
}