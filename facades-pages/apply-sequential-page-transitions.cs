using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfPageEditor with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Define a set of transition types to apply sequentially
                int[] transitions = new int[]
                {
                    PdfPageEditor.BLINDH,
                    PdfPageEditor.BLINDV,
                    PdfPageEditor.BTWIPE,
                    PdfPageEditor.DGLITTER,
                    PdfPageEditor.DISSOLVE,
                    PdfPageEditor.INBOX,
                    PdfPageEditor.LRGLITTER,
                    PdfPageEditor.LRWIPE,
                    PdfPageEditor.OUTBOX,
                    PdfPageEditor.RLWIPE,
                    PdfPageEditor.SPLITHIN,
                    PdfPageEditor.SPLITHOUT,
                    PdfPageEditor.SPLITVIN,
                    PdfPageEditor.SPLITVOUT,
                    PdfPageEditor.TBGLITTER,
                    PdfPageEditor.TBWIPE
                };

                int pageCount = doc.Pages.Count; // Pages are 1‑based
                for (int i = 1; i <= pageCount; i++)
                {
                    // Choose a transition type cyclically for each page
                    int transition = transitions[(i - 1) % transitions.Length];

                    // Apply the transition only to the current page
                    editor.ProcessPages = new int[] { i };
                    editor.TransitionType = transition;
                    editor.TransitionDuration = 2; // Duration in seconds (example)

                    // Commit changes for this page
                    editor.ApplyChanges();
                }
            }

            // Save the modified PDF (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transitions applied and saved to '{outputPath}'.");
    }
}