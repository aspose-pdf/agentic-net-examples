using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfSlideshowCreator
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "slideshow.pdf"; // result PDF with transitions

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // PdfPageEditor is a facade that allows editing page properties
            // such as transition effects and display durations.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor.
                editor.BindPdf(doc);

                int pageCount = doc.Pages.Count; // Aspose.Pdf uses 1‑based indexing

                // Apply a transition and duration to each page.
                for (int i = 1; i <= pageCount; i++)
                {
                    // Restrict the editor to the current page.
                    editor.ProcessPages = new int[] { i };

                    // Choose a transition style based on the page number.
                    // The constants (e.g., BLINDH, DISSOLVE) are defined in PdfPageEditor.
                    if (i % 2 == 1)
                        editor.TransitionType = PdfPageEditor.BLINDH;   // vertical blinds
                    else
                        editor.TransitionType = PdfPageEditor.DISSOLVE; // dissolve effect

                    // Duration of the transition effect (in seconds).
                    editor.TransitionDuration = 2;

                    // How long the page stays visible before moving to the next page (in seconds).
                    editor.DisplayDuration = 5;

                    // Commit the changes for the current page.
                    editor.ApplyChanges();
                }
            }

            // Save the modified document. No SaveOptions are needed because the output
            // format is PDF, which is the default.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Slideshow PDF saved to '{outputPath}'.");
    }
}