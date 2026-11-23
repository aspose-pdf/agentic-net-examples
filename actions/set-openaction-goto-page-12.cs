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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 12 pages
            if (doc.Pages.Count < 12)
            {
                Console.Error.WriteLine("Document has fewer than 12 pages.");
                return;
            }

            // Get the 12th page (Aspose.Pdf uses 1‑based indexing)
            Page targetPage = doc.Pages[12];

            // Create a GoToAction that jumps to the 12th page using XYZExplicitDestination.
            // XYZExplicitDestination(page, left, top, zoom). Using 0,0,0 keeps the default view.
            GoToAction goTo = new GoToAction(new XYZExplicitDestination(targetPage, 0, 0, 0));

            // Replace the existing OpenAction with the new GoToAction
            doc.OpenAction = goTo;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new OpenAction to page 12: {outputPath}");
    }
}