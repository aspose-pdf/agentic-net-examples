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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfPageEditor with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Define the transition types to apply sequentially
                int[] transitionTypes = new int[]
                {
                    PdfPageEditor.BLINDH,   // Vertical blinds
                    PdfPageEditor.DISSOLVE, // Dissolve effect
                    PdfPageEditor.SPLITHIN, // In horizontal split
                    PdfPageEditor.TBWIPE   // Top‑bottom wipe
                };

                // Apply each transition to the corresponding page (1‑based indexing)
                for (int i = 0; i < transitionTypes.Length; i++)
                {
                    int pageNumber = i + 1; // Pages are 1‑based
                    editor.ProcessPages = new int[] { pageNumber }; // Edit only this page
                    editor.TransitionType = transitionTypes[i];
                    editor.TransitionDuration = 2; // Duration in seconds (example)
                    editor.ApplyChanges(); // Commit changes for this page
                }

                // Save the edited PDF with the applied transitions
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with varied page transitions saved to '{outputPath}'.");
    }
}