using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // required for GoToAction, XYZExplicitDestination

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "Zoomed2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create an explicit XYZ destination for the first page with a 200% zoom.
            // The constructor takes (page, left, top, zoom). Using 0,0 for left/top positions the view at the page origin.
            XYZExplicitDestination xyzDest = new XYZExplicitDestination(doc.Pages[1], 0, 0, 2.0);

            // Create a GoToAction that points to the first page and assign the destination.
            GoToAction openAction = new GoToAction(doc.Pages[1])
            {
                Destination = xyzDest
            };

            // Assign the action as the document's OpenAction
            doc.OpenAction = openAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with zoom factor 2.0 to '{outputPath}'.");
    }
}
