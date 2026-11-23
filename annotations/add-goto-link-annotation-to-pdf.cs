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
            // Ensure the document has at least two pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document must contain at least two pages.");
                return;
            }

            // Page 2 will be the destination of the link
            Page destinationPage = doc.Pages[2];

            // Create an XYZ explicit destination:
            // left = NaN, top = NaN keep current position,
            // zoom = 0 means default zoom level.
            XYZExplicitDestination xyzDest = new XYZExplicitDestination(
                destinationPage,
                double.NaN,
                double.NaN,
                0);

            // Use the constructor that accepts a destination (avoids obsolete parameterless ctor)
            GoToAction goTo = new GoToAction(xyzDest);

            // Define the rectangle area on the source page (page 1) where the link will appear
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation (requires source page and rectangle) and assign the action
            LinkAnnotation link = new LinkAnnotation(doc.Pages[1], linkRect)
            {
                Action = goTo
            };

            // Add the annotation to page 1
            doc.Pages[1].Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with link annotation to '{outputPath}'.");
    }
}
