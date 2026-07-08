using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "form.pdf";
        const string outputPath = "form_with_zoom.pdf";
        const double zoomFactor = 1.5; // 150% zoom when the PDF is opened

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor is a Facades class – it implements IDisposable, so use a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF form.
            editor.BindPdf(inputPath);

            // Access the underlying Document object.
            Document doc = editor.Document;

            // Ensure the document has at least one page.
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Create a destination that sets the desired zoom level.
            // XYZExplicitDestination(page, left, top, zoom)
            // left = 0, top = 0 positions the view at the upper‑left corner.
            XYZExplicitDestination zoomDest = new XYZExplicitDestination(doc.Pages[1], 0, 0, zoomFactor);

            // Set the OpenAction so the viewer jumps to the destination when the document opens.
            doc.OpenAction = new GoToAction(zoomDest);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with initial zoom ({zoomFactor * 100}%): {outputPath}");
    }
}