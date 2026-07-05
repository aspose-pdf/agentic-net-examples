using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize PdfPageEditor with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Define a set of transition types to cycle through
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

                // Apply a custom transition to each page based on its index
                for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
                {
                    // Restrict editing to the current page only
                    editor.ProcessPages = new int[] { i };

                    // Choose a transition type cyclically
                    editor.TransitionType = transitions[(i - 1) % transitions.Length];

                    // Set a uniform transition duration (in seconds)
                    editor.TransitionDuration = 2;

                    // Apply the changes for this page
                    editor.ApplyChanges();
                }

                // Save the modified PDF using the facade's Save method
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF with custom page transitions saved to '{outputPdf}'.");
    }
}