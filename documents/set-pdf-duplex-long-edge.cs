using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "DuplexLongEdgeModified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Set duplex printing mode to flip on the long edge
            doc.Duplex = PrintDuplex.DuplexFlipLongEdge;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with DuplexFlipLongEdge to '{outputPath}'.");
    }
}
