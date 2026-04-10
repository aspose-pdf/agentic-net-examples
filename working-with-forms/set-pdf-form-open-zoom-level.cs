using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "form_input.pdf";
        const string outputPath = "form_with_zoom.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to FormEditor (required to work with AcroForm)
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(doc);
                // No additional form editing is performed here
            }

            // Set the document's OpenAction to a GoToAction with a zoom factor.
            // XYZExplicitDestination(page, left, top, zoom) – left/top = 0 positions the view at the upper‑left corner.
            // Zoom factor 1.5 means 150 % magnification.
            doc.OpenAction = new GoToAction(
                new XYZExplicitDestination(doc.Pages[1], 0, 0, 1.5));

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with zoom level set: {outputPath}");
    }
}