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

        // Verify source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialise the PdfPageEditor for the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Process only page 4 (1‑based indexing)
                editor.ProcessPages = new int[] { 4 };

                // Set the transition type to "Cover" (integer value 4) and duration to 1 second
                editor.TransitionType = 4; // Cover transition
                editor.TransitionDuration = 1;

                // Apply the changes and save the result
                editor.ApplyChanges();
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}
