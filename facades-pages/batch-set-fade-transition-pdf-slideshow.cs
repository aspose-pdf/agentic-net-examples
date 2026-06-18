using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source 200‑page PDF
        const string outputPath = "slideshow.pdf"; // result with transitions

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to PdfPageEditor (facade) – also disposable
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Set the transition type to Fade (DISSOLVE constant) and duration to 2 seconds
                editor.TransitionType = PdfPageEditor.DISSOLVE; // Fade effect
                editor.TransitionDuration = 2;                  // 2‑second transition

                // Apply the changes to all pages (default ProcessPages processes every page)
                editor.ApplyChanges();
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Slideshow PDF saved to '{outputPath}'.");
    }
}