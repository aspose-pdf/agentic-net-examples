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
        const string soundPath  = "notification.wav"; // must be a valid WAV file

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define a tiny rectangle (invisible area) where the annotation will be placed
                // Fully qualify the Rectangle type to avoid ambiguity with System.Drawing
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

                // Create the SoundAnnotation. The constructor takes the page, rectangle and sound file path.
                SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundPath)
                {
                    // Use the speaker icon (optional)
                    Icon = SoundIcon.Speaker
                    // Note: The Activation property is not available in the current Aspose.PDF version.
                    // SoundAnnotation will play when the user clicks the annotation. If page‑open playback
                    // is required, a RichMediaAnnotation (ScreenAnnotation) with appropriate activation
                    // settings should be used instead.
                };

                // Add the annotation to the page's annotation collection.
                page.Annotations.Add(soundAnn);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with sound annotations saved to '{outputPath}'.");
    }
}
