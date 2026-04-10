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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // -----------------------------------------------------------------
                // NOTE: Aspose.Pdf does not expose a per‑page "Title" property via
                // PageInfo. If you have a custom way of storing titles (e.g., in
                // page annotations or a custom dictionary) replace the placeholder
                // below with that logic. For the purpose of a compilable example we
                // fall back to an empty string, which results in the minimum duration.
                // -----------------------------------------------------------------
                string title = string.Empty; // replace with actual title retrieval if needed

                // Determine transition duration based on title length (e.g., 1 second per character)
                int duration = title.Length;
                if (duration == 0) duration = 1; // ensure a minimum duration

                // Apply a transition to the current page using PdfPageEditor.
                // TransitionType expects an integer – 1 corresponds to the "Split" style.
                using (PdfPageEditor editor = new PdfPageEditor(doc))
                {
                    editor.ProcessPages = new int[] { i }; // 1‑based page index
                    editor.TransitionType = 1; // Split transition
                    editor.TransitionDuration = duration; // duration in seconds
                    editor.ApplyChanges();
                }
            }

            // Save the modified PDF (using the standard Save method)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page transitions to '{outputPath}'.");
    }
}
