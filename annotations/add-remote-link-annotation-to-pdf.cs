using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string remotePdfPath = "Resources/Guide.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the clickable area (coordinates are in points)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a remote GoTo action that opens the external PDF at page 1
            // Use the constructor that accepts the file path and the target page number.
            GoToRemoteAction remoteAction = new GoToRemoteAction(remotePdfPath, 1);

            // Create the link annotation, assign the action, and add it to the page
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                Action = remoteAction
            };
            page.Annotations.Add(link);

            // Save the modified document (PDF format by default)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with link annotation to '{outputPdfPath}'.");
    }
}
