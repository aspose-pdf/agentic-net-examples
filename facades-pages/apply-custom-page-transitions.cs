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
            // Initialize the PdfPageEditor facade with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Choose a transition style based on the page index.
                    // This example cycles through three different transitions.
                    int transition;
                    switch (i % 3)
                    {
                        case 0:
                            transition = PdfPageEditor.BLINDH;      // Vertical blinds
                            break;
                        case 1:
                            transition = PdfPageEditor.DISSOLVE;   // Dissolve effect
                            break;
                        default:
                            transition = PdfPageEditor.SPLITVOUT;  // Out vertical split
                            break;
                    }

                    // Specify the page to edit
                    editor.ProcessPages = new int[] { i };
                    // Apply the chosen transition type
                    editor.TransitionType = transition;
                    // Set the duration of the transition (seconds)
                    editor.TransitionDuration = 2;

                    // Commit changes for the current page
                    editor.ApplyChanges();
                }

                // Save the modified PDF with the applied transitions
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Presentation PDF saved to '{outputPath}'.");
    }
}