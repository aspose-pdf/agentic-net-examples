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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor bound to the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Build an array of odd‑numbered page indices (1‑based indexing)
            List<int> oddPages = new List<int>();
            for (int i = 1; i <= doc.Pages.Count; i += 2)
            {
                oddPages.Add(i);
            }

            // Specify which pages the editor should process
            editor.ProcessPages = oddPages.ToArray();

            // Choose a transition style (any of the provided constants)
            // Example: vertical blinds transition
            editor.TransitionType = PdfPageEditor.BLINDV;

            // Set the duration (in seconds) for the transition effect
            editor.TransitionDuration = 2; // 2 seconds

            // Apply the changes to the selected pages
            editor.ApplyChanges();

            // Save the modified PDF to the output file
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with transitions on odd pages: {outputPath}");
    }
}