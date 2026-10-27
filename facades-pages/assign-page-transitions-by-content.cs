using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_transitions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // PdfPageEditor is the Facades class that allows setting page transitions
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Define transition type per page (page numbers are 1‑based)
                // Transition types are integer values defined by the PDF specification.
                // Example values:
                // 0 – No transition
                // 1 – Replace
                // 2 – Split
                // 3 – Blinds
                // 4 – Box
                // 5 – Wipe
                // 6 – Dissolve
                // 7 – Glitter
                // 8 – Fly
                // 9 – Push
                // 10 – Cover
                // 11 – Uncover
                // 12 – Fade
                var pageTransitions = new Dictionary<int, int>
                {
                    { 1, 0 },  // No transition on page 1
                    { 2, 4 },  // Box transition on page 2
                    { 3, 6 },  // Dissolve transition on page 3
                    // Add more mappings as needed
                };

                foreach (var kvp in pageTransitions)
                {
                    int pageNumber = kvp.Key;      // 1‑based page index
                    int transition = kvp.Value;    // integer representing the transition style

                    // Apply the transition only to the specified page
                    editor.ProcessPages = new int[] { pageNumber };
                    editor.TransitionType = transition;
                    editor.TransitionDuration = 1; // duration in seconds (adjust as required)

                    editor.ApplyChanges();
                }

                // Persist all changes to the output file
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with page transitions to '{outputPath}'.");
    }
}