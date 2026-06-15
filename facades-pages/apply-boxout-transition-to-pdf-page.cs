using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (a SaveableFacade) to edit page transitions.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF.
            editor.BindPdf(inputPath);

            // Edit only page 2 (Aspose.Pdf uses 1‑based page indexing).
            editor.ProcessPages = new int[] { 2 };

            // Set transition type to BoxOut (outward box) and duration to 3 seconds.
            editor.TransitionType = PdfPageEditor.OUTBOX;   // BoxOut effect
            editor.TransitionDuration = 3;                  // 3‑second duration

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}