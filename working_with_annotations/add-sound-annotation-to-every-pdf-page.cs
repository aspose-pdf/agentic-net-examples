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
        const string soundFile  = "notification.wav"; // path to the sound file (WAV, MP3, etc.)

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

        // Load the PDF document (using the standard constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define a small invisible rectangle for the annotation.
                // Fully qualify to avoid ambiguity with System.Drawing.Rectangle.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 1, 1);

                // Create the SoundAnnotation on the current page.
                // Constructor: SoundAnnotation(Page, Rectangle, string soundFile)
                SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundFile)
                {
                    // Use the speaker icon (optional).
                    Icon = SoundIcon.Speaker,

                    // Optionally set a tooltip or contents.
                    Contents = "Page load notification"
                };

                // Add the annotation to the page's annotation collection.
                page.Annotations.Add(soundAnn);
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with sound annotations saved to '{outputPath}'.");
    }
}