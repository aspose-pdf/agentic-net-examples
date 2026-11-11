using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Verify that the document contains at least six pages.
        if (pdfDocument.Pages.Count < 6)
        {
            Console.Error.WriteLine("The document does not contain page 6.");
            return;
        }

        // Use PdfPageEditor to set a zero‑second transition on page 6.
        // The older PageTransition class is not available in the bundled version.
        using (PdfPageEditor editor = new PdfPageEditor(pdfDocument))
        {
            editor.ProcessPages = new int[] { 6 };   // 1‑based page index
            editor.TransitionType = 0;               // 0 = No transition (any valid type works when duration is 0)
            editor.TransitionDuration = 0;           // seconds – disables the transition animation
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Transition disabled on page 6. Saved to '{outputPath}'.");
    }
}
