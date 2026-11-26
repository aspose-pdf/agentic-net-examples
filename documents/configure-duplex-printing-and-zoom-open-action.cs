using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Actions and Destination live here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "DuplexZoomed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Set duplex printing mode to flip on the long edge
            doc.Duplex = PrintDuplex.DuplexFlipLongEdge;

            // Set the document's open action to zoom to 125% on the first page
            // XYZExplicitDestination(page, left, top, zoom)
            // Using 0,0 for left/top positions (top‑left corner) – adjust if needed.
            doc.OpenAction = new GoToAction(new XYZExplicitDestination(doc.Pages[1], 0, 0, 1.25));

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved duplex and zoomed PDF to '{outputPath}'.");
    }
}
