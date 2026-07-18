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
        const string soundFile  = "notification.wav"; // path to the sound to be played

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(soundFile))
        {
            Console.Error.WriteLine($"Sound file not found: {soundFile}");
            return;
        }

        try
        {
            // Load the PDF (using statement ensures deterministic disposal)
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Define a tiny rectangle (in points) where the annotation will be placed.
                    // The rectangle can be invisible; its size does not affect playback.
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 1, 1);

                    // Create a SoundAnnotation that references the sound file.
                    SoundAnnotation sound = new SoundAnnotation(page, rect, soundFile);

                    // Optional: set the icon that appears if the annotation is visible.
                    sound.Icon = SoundIcon.Speaker;

                    // Add the annotation to the page. The bool parameter handles page rotation.
                    page.Annotations.Add(sound, true);
                }

                // Save the modified PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with sound annotations saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}