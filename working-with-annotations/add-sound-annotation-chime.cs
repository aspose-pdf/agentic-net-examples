using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, and the sound file (e.g., a .wav or .mp3)
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_sound.pdf";
        const string soundFile = "chime.wav";

        // The page number (1‑based) on which the sound should play when the page becomes visible
        const int targetPageNumber = 3;

        // Verify that the required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(soundFile))
        {
            Console.Error.WriteLine($"Sound file not found: {soundFile}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the requested page exists
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Get the target page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[targetPageNumber];

            // Define the annotation rectangle (coordinates are in points; lower‑left to upper‑right)
            // Here we place a small icon near the top‑right corner of the page
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;
            double iconSize   = 20; // size of the sound icon

            // Rectangle: (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                pageWidth - iconSize - 10, // llx (10 points margin from right edge)
                pageHeight - iconSize - 10, // lly (10 points margin from top edge)
                pageWidth - 10,             // urx
                pageHeight - 10);            // ury

            // Create the SoundAnnotation.
            // The constructor takes the page, rectangle, and path to the sound file.
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundFile)
            {
                // Use the speaker icon to indicate an audible cue
                Icon = SoundIcon.Speaker,

                // Optional: a tooltip that appears when the user hovers over the icon
                Contents = "Page‑arrival chime"
            };

            // Add the annotation to the page's annotation collection.
            // The overload without the rotation flag is sufficient here.
            page.Annotations.Add(soundAnn);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Sound annotation added to page {targetPageNumber}. Saved as '{outputPdf}'.");
    }
}