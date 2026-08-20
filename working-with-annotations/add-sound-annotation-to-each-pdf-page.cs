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
        const string soundFile  = "notification.wav"; // path to the sound file

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
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define a small rectangle where the annotation will be placed
                // (left, bottom, right, top)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 50, 70, 70);

                // Create a SoundAnnotation that plays the specified sound file
                SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundFile)
                {
                    // Optional: set the icon to a speaker
                    Icon = SoundIcon.Speaker,
                    // Activate the sound when the page becomes visible
                    // (requires Aspose.Pdf 20.10+; if not available, the default activation is click)
                    // ActivationEvent = RichMediaAnnotation.ActivationEvent.PageVisible
                };

                // Add the annotation to the page
                page.Annotations.Add(soundAnn);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with sound annotations saved to '{outputPath}'.");
    }
}