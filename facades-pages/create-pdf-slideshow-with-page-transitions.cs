using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfSlideshowCreator
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "slideshow.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the page editor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the document to the editor
                editor.BindPdf(doc);

                // -----------------------------------------------------------------
                // Page 1 – set a vertical blinds transition, 2‑second effect,
                // display the page for 5 seconds before moving to the next page.
                // -----------------------------------------------------------------
                editor.ProcessPages      = new int[] { 1 };
                editor.TransitionType    = PdfPageEditor.BLINDV;   // vertical blinds
                editor.TransitionDuration = 2;                    // seconds for the transition effect
                editor.DisplayDuration    = 5;                    // seconds the page stays visible
                editor.ApplyChanges();

                // -----------------------------------------------------------------
                // Page 2 – set a dissolve transition, 1‑second effect,
                // display the page for 4 seconds.
                // -----------------------------------------------------------------
                editor.ProcessPages      = new int[] { 2 };
                editor.TransitionType    = PdfPageEditor.DISSOLVE;
                editor.TransitionDuration = 1;
                editor.DisplayDuration    = 4;
                editor.ApplyChanges();

                // -----------------------------------------------------------------
                // Page 3 – set a left‑to‑right wipe transition, 3‑second effect,
                // display the page for 6 seconds.
                // -----------------------------------------------------------------
                editor.ProcessPages      = new int[] { 3 };
                editor.TransitionType    = PdfPageEditor.LRWIPE; // left‑right wipe
                editor.TransitionDuration = 3;
                editor.DisplayDuration    = 6;
                editor.ApplyChanges();

                // Add more pages as needed following the same pattern.
            }

            // Save the modified PDF as a slideshow
            doc.Save(outputPath);
        }

        Console.WriteLine($"Slideshow PDF saved to '{outputPath}'.");
    }
}