using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfSlideshowCreator
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "slideshow.pdf"; // result PDF with transitions

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the source PDF using the standard Document constructor (load rule)
        using (Document doc = new Document(inputPdf))
        {
            // Initialize PdfPageEditor (facade) and bind the loaded document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Loop through each page (1‑based indexing) and apply transition settings
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Specify that only the current page will be edited
                    editor.ProcessPages = new int[] { pageNum };

                    // Set transition type (e.g., vertical blinds) and its duration (seconds)
                    editor.TransitionType = PdfPageEditor.BLINDV; // vertical blinds
                    editor.TransitionDuration = 2;                // 2‑second transition effect

                    // Set how long the page stays visible before moving to the next page
                    editor.DisplayDuration = 5; // 5‑second display time per page

                    // Apply the changes to the current page
                    editor.ApplyChanges();
                }

                // Save the modified PDF (save rule)
                doc.Save(outputPdf);
            }
        }

        Console.WriteLine($"Slideshow PDF created: {outputPdf}");
    }
}