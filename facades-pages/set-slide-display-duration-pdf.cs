using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF (must exist) and output PDF paths
        const string inputPdf  = "presentation.pdf";
        const string outputPdf = "presentation_with_timing.pdf";

        // Desired display duration in seconds for each slide
        const int slideDurationSeconds = 5; // e.g., 5 seconds per page

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfPageEditor (a SaveableFacade) to edit page‑level properties
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF to the editor
            editor.BindPdf(inputPdf);

            // Set the display duration for pages (in seconds)
            editor.DisplayDuration = slideDurationSeconds;

            // Optional: set a transition effect and its duration
            // editor.TransitionType = PdfPageEditor.BLINDV; // vertical blinds
            // editor.TransitionDuration = 2;               // 2‑second transition

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with slide timing to '{outputPdf}'.");
    }
}