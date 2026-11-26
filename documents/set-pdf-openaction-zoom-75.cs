using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // required for GoToAction and destination types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "Zoomed075.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a GoToAction that points to the first page (pages are 1‑based)
            GoToAction openAction = new GoToAction(doc.Pages[1]);

            // Set the destination with a zoom factor of 0.75 (75%).
            // XYZExplicitDestination(page, x, y, zoom) – x and y are coordinates.
            openAction.Destination = new XYZExplicitDestination(doc.Pages[1], 0, 0, 0.75);

            // Assign the action as the document's OpenAction
            doc.OpenAction = openAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with zoom factor 0.75 to '{outputPath}'.");
    }
}
