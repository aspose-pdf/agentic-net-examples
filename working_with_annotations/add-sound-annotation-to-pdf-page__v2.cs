using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_sound.pdf";
        const int    targetPage = 2;                     // page on which the sound will be triggered
        const string soundFile   = "chime.wav";          // path to the sound file (must be a supported audio format)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(soundFile))
        {
            Console.Error.WriteLine($"Sound file not found: {soundFile}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists (Aspose.Pdf uses 1‑based indexing)
            if (targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPage} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            Page page = doc.Pages[targetPage];

            // Define a small invisible rectangle where the annotation will be placed.
            // The rectangle can be positioned anywhere; using a 0‑size rectangle makes it invisible.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create the SoundAnnotation. The constructor requires the page, rectangle, and the sound file path.
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundFile)
            {
                // Use the speaker icon to indicate a sound annotation.
                Icon = SoundIcon.Speaker,

                // Optional: set a title that appears in the annotation's popup (if the user clicks it).
                Title = "Page Chime",

                // Optional: provide a short description.
                Contents = "A chime will play when this page becomes visible."
            };

            // Add the annotation to the page's annotation collection.
            // The second parameter (considerRotation) is omitted; default behavior is sufficient.
            page.Annotations.Add(soundAnn);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with sound annotation: {outputPath}");
    }
}