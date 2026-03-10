using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputOfdPath  = "input.ofd";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputOfdPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputOfdPath}");
            return;
        }

        // Load OFD file (OFD is input‑only, so we load it as a PDF document)
        using (Document doc = new Document(inputOfdPath, new OfdLoadOptions()))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Create a link annotation on the first page
            // Rectangle defines the clickable area (left, bottom, right, top)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            LinkAnnotation link = new LinkAnnotation(doc.Pages[1], linkRect)
            {
                // Optional visual appearance
                Color = Aspose.Pdf.Color.Blue,
                // Tooltip text shown when hovering
                Contents = "Go to page 2"
            };

            // Set the action to navigate to page 2 of the same document
            // Use explicit destination via GoToAction (Destination class does not exist)
            link.Action = new GoToAction(doc.Pages[2]);

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(link);

            // Save the modified document as PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPdfPath}'.");
    }
}