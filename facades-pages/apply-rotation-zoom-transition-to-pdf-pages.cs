using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PresentationPdfProcessor
{
    static void Main()
    {
        const string inputPdf  = "presentation_input.pdf";
        const string outputPdf = "presentation_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the source PDF and edit its pages using PdfPageEditor (Facade API)
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Bind the source PDF file to the editor
            pageEditor.BindPdf(inputPdf);

            // Example: apply rotation, zoom, and a transition effect to all pages
            // Rotation must be 0, 90, 180 or 270 degrees
            pageEditor.Rotation = 90;               // rotate 90 degrees clockwise
            pageEditor.Zoom = 1.5f;                  // 150% zoom (1.0 = 100%) – float literal
            pageEditor.TransitionType = PdfPageEditor.BLINDH; // vertical blinds transition
            pageEditor.TransitionDuration = 2;      // transition lasts 2 seconds
            pageEditor.DisplayDuration = 5;         // each page displayed for 5 seconds

            // Apply the configured changes to the document pages
            pageEditor.ApplyChanges();

            // Save the modified PDF to the output path
            pageEditor.Save(outputPdf);
        }

        Console.WriteLine($"Presentation PDF saved to '{outputPdf}'.");
    }
}
