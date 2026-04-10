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

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfPageEditor with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Iterate through all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Choose a transition type based on page index (example: cycle through three types)
                    int transition;
                    switch (i % 3)
                    {
                        case 0:
                            transition = PdfPageEditor.BLINDH;   // vertical blinds
                            break;
                        case 1:
                            transition = PdfPageEditor.DISSOLVE; // dissolve
                            break;
                        default:
                            transition = PdfPageEditor.SPLITVOUT; // out vertical split
                            break;
                    }

                    // Specify which page to edit
                    editor.ProcessPages = new int[] { i };
                    // Apply the chosen transition and set its duration (seconds)
                    editor.TransitionType = transition;
                    editor.TransitionDuration = 2; // 2‑second transition
                    // Commit changes for this page
                    editor.ApplyChanges();
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Presentation PDF saved to '{outputPath}'.");
    }
}