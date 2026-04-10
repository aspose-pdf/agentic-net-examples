using System;
using System.IO;
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

        // PdfPageEditor is a facade that edits page‑level properties such as transitions.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Define three transition types to cycle through.
            int[] transitionTypes = new int[]
            {
                PdfPageEditor.BLINDH,   // Vertical blinds
                PdfPageEditor.BLINDV,   // Horizontal blinds
                PdfPageEditor.DISSOLVE  // Dissolve effect
            };

            // Total number of pages in the document.
            int pageCount = editor.GetPages();

            // Apply a different transition to each page based on (pageIndex‑1) % 3.
            for (int i = 1; i <= pageCount; i++)
            {
                // Restrict editing to the current page only.
                editor.ProcessPages = new int[] { i };

                // Select the transition type for this page.
                editor.TransitionType = transitionTypes[(i - 1) % transitionTypes.Length];

                // Optionally set a transition duration (in seconds).
                editor.TransitionDuration = 2;

                // Commit the change for the current page.
                editor.ApplyChanges();
            }

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page transitions to '{outputPath}'.");
    }
}