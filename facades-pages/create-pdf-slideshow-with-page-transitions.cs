using System;
using System.IO;
using Aspose.Pdf.Facades;

class PdfSlideshowCreator
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "slideshow.pdf";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Example: define transition type and duration for each page.
        // Transition types are defined as constant fields in PdfPageEditor.
        // Here we use a simple pattern: alternating between two transitions.
        // TransitionDuration is in seconds, DisplayDuration is also in seconds.
        // Adjust the values to match your presentation needs.
        const int transitionDuration = 2;   // seconds for the transition effect (int required)
        const int displayDuration    = 5;   // seconds each page stays visible (int required)

        // Create and configure the PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file
            editor.BindPdf(inputPdf);

            // Get total number of pages in the document
            int pageCount = editor.GetPages();

            // Apply transition and display settings to each page
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // Specify which page to edit
                editor.ProcessPages = new int[] { pageNumber };

                // Choose a transition type based on page number (example pattern)
                // Available constants: BLINDH, BLINDV, BTWIPE, DGLITTER, DISSOLVE,
                // INBOX, LRGLITTER, LRWIPE, OUTBOX, RLWIPE, SPLITHIN, SPLITHOUT,
                // SPLITVIN, SPLITVOUT, TBGLITTER, TBWIPE
                editor.TransitionType = (pageNumber % 2 == 0) ? PdfPageEditor.BLINDH : PdfPageEditor.DISSOLVE;

                // Set the duration of the transition effect (in seconds, int required)
                editor.TransitionDuration = transitionDuration;

                // Set how long the page is displayed before moving to the next one (in seconds, int required)
                editor.DisplayDuration = displayDuration;

                // Apply the changes for the current page
                editor.ApplyChanges();
            }

            // Save the modified PDF as a slideshow
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Slideshow PDF created: {outputPdf}");
    }
}
