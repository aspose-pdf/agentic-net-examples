using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_fade.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Apply a Fade (Dissolve) transition to every page using PdfPageEditor
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Process all pages – create an array with page numbers (1‑based)
                editor.ProcessPages = Enumerable.Range(1, doc.Pages.Count).ToArray();

                // TransitionType = 5 corresponds to the "Dissolve" (fade‑like) effect.
                // TransitionDuration is expressed in seconds.
                editor.TransitionType = 5;          // Fade/Dissolve
                editor.TransitionDuration = 2;      // 2 seconds

                editor.ApplyChanges();
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with fade transition: '{outputPath}'.");
    }
}
