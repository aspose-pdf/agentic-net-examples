using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "form_input.pdf";
        const string outputPdf = "form_with_zoom.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the PDF (which contains a form) inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least one page.
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Choose the page that will be displayed when the document is opened.
            // Here we use the first page (1‑based indexing).
            Page firstPage = doc.Pages[1];

            // Set the initial view zoom level.
            // XYZExplicitDestination takes (page, left, top, zoomFactor).
            // A zoom factor of 1.0 = 100%, 1.5 = 150%, etc.
            double zoomFactor = 1.5; // 150% zoom for better readability
            var destination = new XYZExplicitDestination(firstPage, 0, 0, zoomFactor);

            // Assign the destination to the document's OpenAction.
            // When the PDF is opened in a viewer, it will navigate to the specified page
            // with the defined zoom level.
            doc.OpenAction = new GoToAction(firstPage) { Destination = destination };

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with initial zoom set to {1.5 * 100}% at '{outputPdf}'.");
    }
}