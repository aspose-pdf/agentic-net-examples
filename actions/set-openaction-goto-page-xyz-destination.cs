using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Actions and destinations live in the Annotations namespace

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create an explicit XYZ destination on page 10 (1‑based index)
            // left = 0, top = 0, zoom = 1 (100%)
            XYZExplicitDestination destination = new XYZExplicitDestination(10, 0, 0, 1);

            // Wrap the destination in a GoToAction
            GoToAction openAction = new GoToAction(destination);

            // Assign the action to the document's OpenAction property
            doc.OpenAction = openAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with OpenAction to '{outputPath}'.");
    }
}
