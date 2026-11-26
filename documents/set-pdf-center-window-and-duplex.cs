using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "CenteredDuplex.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, set window properties, and save
        using (Document doc = new Document(inputPath))
        {
            // Center the PDF viewer window when opened
            doc.CenterWindow = true;

            // Set duplex printing mode to flip long edge
            doc.Duplex = PrintDuplex.DuplexFlipLongEdge;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}
