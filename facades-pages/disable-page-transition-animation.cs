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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Specify that only page 6 should be edited (1‑based indexing)
                editor.ProcessPages = new int[] { 6 };

                // Set the transition duration to zero seconds to disable animation
                editor.TransitionDuration = 0;

                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transition duration set to zero on page 6. Saved to '{outputPath}'.");
    }
}