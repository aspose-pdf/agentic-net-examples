using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
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

        // Load the source PDF
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Create a PdfPageEditor to modify page transition settings
            using (Aspose.Pdf.Facades.PdfPageEditor editor = new Aspose.Pdf.Facades.PdfPageEditor(doc))
            {
                // Apply a transition to each page.
                // TransitionType values are integer codes defined by the PDF spec.
                // Example: 4 = Cover transition, 2 = Dissolve, etc.
                // TransitionDuration is expressed in seconds.
                for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
                {
                    editor.ProcessPages = new int[] { pageNumber };   // 1‑based page index
                    editor.TransitionType = 4;                       // Cover transition
                    editor.TransitionDuration = 2;                   // 2‑second duration
                    editor.ApplyChanges();                           // Apply to the current page
                }

                // Save the modified document with transitions applied
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Slideshow PDF saved to '{outputPath}'.");
    }
}