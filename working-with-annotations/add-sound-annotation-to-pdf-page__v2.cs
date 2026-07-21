using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_with_sound.pdf"; // result PDF
        const string soundPath  = "chime.wav";          // sound file (WAV, MP3, etc.)
        const int targetPageNumber = 3;                 // page on which the sound should play

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(soundPath))
        {
            Console.Error.WriteLine($"Sound file not found: {soundPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Validate page number (Aspose.Pdf uses 1‑based indexing)
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Get the target page
            Page page = doc.Pages[targetPageNumber];

            // Define the annotation rectangle (position and size on the page)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

            // Create the SoundAnnotation.
            // Constructor: SoundAnnotation(Page, Rectangle, string soundFile)
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundPath)
            {
                // Optional: choose an icon (Speaker or Mic)
                Icon = SoundIcon.Speaker,

                // Optional: set a tooltip or description
                Title   = "Page Chime",
                Contents = "A chime will play when this page becomes visible."
            };

            // Add the annotation to the page's annotation collection.
            // The overload without the rotation flag is sufficient here.
            page.Annotations.Add(soundAnn);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with sound annotation: {outputPath}");
    }
}