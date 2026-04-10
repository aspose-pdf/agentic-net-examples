using System;
using System.IO;
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

        // PdfPageEditor is a facade that implements IDisposable.
        // It can edit page rotation, zoom, transition effects, etc.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file to the editor.
            editor.BindPdf(inputPdf);

            // Apply the same settings to all pages.
            // Rotation must be 0, 90, 180 or 270 degrees.
            editor.Rotation = 90;               // rotate each page 90° clockwise
            editor.Zoom = 1.5f;                  // 150% zoom (1.0 = 100%) – float literal
            
            // Choose a transition effect for slide changes.
            // Available constants are defined in PdfPageEditor (e.g., BLINDV, DISSOLVE, etc.).
            editor.TransitionType = PdfPageEditor.BLINDV; // vertical blinds transition
            editor.TransitionDuration = 2;                // transition lasts 2 seconds
            editor.DisplayDuration = 5;                   // each slide displayed for 5 seconds

            // If you need to affect only specific pages, set ProcessPages accordingly.
            // Example: editor.ProcessPages = new int[] { 1, 3, 5 }; // only pages 1,3,5
            // By default all pages are processed.

            // Apply the configured changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Presentation PDF saved to '{outputPdf}'.");
    }
}
