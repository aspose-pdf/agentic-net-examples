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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Apply a different transition to each page (Fade, BoxOut, Cover)
        for (int i = 1; i <= pdfDocument.Pages.Count; i++)
        {
            int transitionType;
            switch ((i - 1) % 3)
            {
                case 0: // Fade
                    transitionType = 0; // Fade (integer value defined by Aspose.Pdf)
                    break;
                case 1: // BoxOut
                    transitionType = 1; // BoxOut
                    break;
                default: // Cover
                    transitionType = 4; // Cover (as documented for Aspose.Pdf)
                    break;
            }

            // Use PdfPageEditor to set the transition for the current page.
            using (PdfPageEditor editor = new PdfPageEditor(pdfDocument))
            {
                editor.ProcessPages = new int[] { i }; // 1‑based page index
                editor.TransitionType = transitionType;
                editor.TransitionDuration = 1; // seconds
                editor.ApplyChanges();
            }
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Transitions applied and saved to '{outputPath}'.");
    }
}
