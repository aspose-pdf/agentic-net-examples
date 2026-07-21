using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // Facade API for page editing

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_transitions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Example: set different transition effects for the first three pages
                // Transition constants are defined in PdfPageEditor (int values)
                // Fade  -> DISSOLVE
                // BoxOut -> OUTBOX
                // Cover  -> INBOX

                // Page 1 – Fade (Dissolve)
                editor.ProcessPages = new int[] { 1 };
                editor.TransitionType = PdfPageEditor.DISSOLVE;   // Fade effect
                editor.TransitionDuration = 2;                    // 2 seconds
                editor.ApplyChanges();

                // Page 2 – BoxOut
                editor.ProcessPages = new int[] { 2 };
                editor.TransitionType = PdfPageEditor.OUTBOX;    // BoxOut effect
                editor.TransitionDuration = 2;
                editor.ApplyChanges();

                // Page 3 – Cover
                editor.ProcessPages = new int[] { 3 };
                editor.TransitionType = PdfPageEditor.INBOX;     // Cover effect
                editor.TransitionDuration = 2;
                editor.ApplyChanges();

                // If you want the same transition for all remaining pages,
                // you can set ProcessPages to null (default) and assign the values once.
                // Example for pages 4..end:
                // editor.ProcessPages = null; // all pages
                // editor.TransitionType = PdfPageEditor.DISSOLVE;
                // editor.TransitionDuration = 2;
                // editor.ApplyChanges();

                // Save the modified document
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with transitions to '{outputPath}'.");
    }
}