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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 3 exists (Aspose.Pdf pages are 1‑based)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document contains fewer than 3 pages.");
                return;
            }

            // Create a destination that opens page 3 with default view parameters
            XYZExplicitDestination destination = new XYZExplicitDestination(doc.Pages[3], 0, 0, 0);

            // Create a GoTo action using the destination
            GoToAction openAction = new GoToAction(destination);

            // Set the document's open action
            doc.OpenAction = openAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with open action to '{outputPath}'.");
    }
}