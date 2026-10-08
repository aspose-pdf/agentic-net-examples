using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_transitions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // PdfPageEditor works on an existing Document instance
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // -----------------------------------------------------------------
                // Fade transition on page 1 (TransitionType = 0)
                // -----------------------------------------------------------------
                if (doc.Pages.Count >= 1)
                {
                    editor.ProcessPages = new int[] { 1 };   // 1‑based page number
                    editor.TransitionType = 0;               // 0 = Fade
                    editor.TransitionDuration = 2;           // seconds
                    editor.ApplyChanges();
                }

                // -----------------------------------------------------------------
                // BoxOut transition on page 2 (TransitionType = 1)
                // -----------------------------------------------------------------
                if (doc.Pages.Count >= 2)
                {
                    editor.ProcessPages = new int[] { 2 };
                    editor.TransitionType = 1;               // 1 = BoxOut
                    editor.TransitionDuration = 2;
                    editor.ApplyChanges();
                }

                // -----------------------------------------------------------------
                // Cover transition on page 3 (TransitionType = 2)
                // -----------------------------------------------------------------
                if (doc.Pages.Count >= 3)
                {
                    editor.ProcessPages = new int[] { 3 };
                    editor.TransitionType = 2;               // 2 = Cover
                    editor.TransitionDuration = 2;
                    editor.ApplyChanges();
                }

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with transitions to '{outputPath}'.");
    }
}
