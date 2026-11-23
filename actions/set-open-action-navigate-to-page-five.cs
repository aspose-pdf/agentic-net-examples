using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least five pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("Document does not contain five pages.");
                return;
            }

            // Retrieve page five (Aspose.Pdf uses 1‑based indexing)
            Page pageFive = doc.Pages[5];

            // Create an XYZ explicit destination (left = 0, top = 0, zoom = 0)
            XYZExplicitDestination destination = new XYZExplicitDestination(pageFive, 0, 0, 0);

            // Create a GoTo action that points to the destination
            GoToAction openAction = new GoToAction(destination);

            // Assign the action as the document's open action
            doc.OpenAction = openAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with open action to '{outputPath}'.");
    }
}
