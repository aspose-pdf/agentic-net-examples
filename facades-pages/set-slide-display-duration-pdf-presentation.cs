using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Required for PdfPageEditor

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "presentation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define display duration (in seconds) for each slide.
        // If fewer durations than pages are provided, the last value is reused.
        int[] slideDurations = { 5, 8, 4, 6 }; // example values

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Iterate using 1‑based page indexing (Aspose.Pdf requirement).
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Choose duration for the current page.
                int duration = i <= slideDurations.Length
                               ? slideDurations[i - 1]
                               : slideDurations[slideDurations.Length - 1];

                // Apply a transition to the current page using PdfPageEditor.
                // TransitionType is set via its integer value (2 corresponds to Fade in Aspose.Pdf).
                using (PdfPageEditor editor = new PdfPageEditor(doc))
                {
                    editor.ProcessPages = new int[] { i }; // target page (1‑based)
                    editor.TransitionType = 2;            // Fade transition (integer code)
                    editor.TransitionDuration = duration; // duration in seconds
                    editor.ApplyChanges();
                }
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Presentation PDF saved to '{outputPath}'.");
    }
}
