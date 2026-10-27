using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "slideshow.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with the core API (Document)
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------------------------
            // 1. Set a page‑transition (e.g., Fade) and display duration for **all** pages
            //    using PdfPageEditor (the correct way to set these properties).
            // -------------------------------------------------------------------
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Prepare an array with all page numbers (1‑based indexing)
                int[] allPages = new int[doc.Pages.Count];
                for (int i = 0; i < allPages.Length; i++)
                    allPages[i] = i + 1;

                editor.ProcessPages = allPages;
                editor.TransitionType = 11;          // 11 = Fade (PDF spec value)
                editor.TransitionDuration = 2;      // transition duration in seconds
                editor.DisplayDuration = 5;         // how long each page stays visible (seconds)
                editor.ApplyChanges();               // apply the settings
            }

            // Save the modified PDF – it now behaves as a slideshow
            doc.Save(outputPath);
        }

        Console.WriteLine($"Slideshow PDF created: {outputPath}");
    }
}
