using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, Layer, etc.

class Program
{
    static void Main()
    {
        // Input PDF that may contain layers.
        const string inputPath = "input.pdf";

        // Output PDF that will preserve any existing layers.
        const string outputPath = "layers.pdf";

        // Verify the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Always wrap Document in a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // No additional layer manipulation is required; saving the document
            // preserves any existing layers.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with layers to '{outputPath}'.");
    }
}