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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade with the loaded document.
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Iterate over all pages (1‑based indexing).
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Restrict editing to the current page.
                    editor.ProcessPages = new int[] { i };

                    // Set the display duration: page index seconds.
                    editor.DisplayDuration = i; // 1 sec for page 1, 2 sec for page 2, etc.

                    // Apply the change to the current page.
                    editor.ApplyChanges();
                }

                // Save the modified PDF to the output file.
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with per‑page display durations to '{outputPath}'.");
    }
}