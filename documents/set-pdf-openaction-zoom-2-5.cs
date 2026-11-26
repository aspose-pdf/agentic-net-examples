using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for GoToAction and explicit destinations

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "Zoomed2_5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create an explicit XYZ destination on the first page with a zoom factor of 2.5 (250%).
            // XYZExplicitDestination(page, left, top, zoom)
            var xyzDest = new XYZExplicitDestination(doc.Pages[1], 0, 0, 2.5);

            // Assign the destination to the document's OpenAction via a GoToAction.
            doc.OpenAction = new GoToAction(xyzDest);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with zoom factor 2.5 to '{outputPath}'.");
    }
}
