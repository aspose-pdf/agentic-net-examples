using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_sound.pdf";
        const string soundPath = "tone.wav"; // notification tone file (WAV, MP3, etc.)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(soundPath))
        {
            Console.Error.WriteLine($"Sound file not found: {soundPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing, foreach works correctly)
            foreach (Page page in doc.Pages)
            {
                // Define an invisible rectangle for the annotation (zero size)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

                // Create a SoundAnnotation on the current page.
                // Constructor signature: SoundAnnotation(Page page, Rectangle rect, string soundFilePath)
                SoundAnnotation sound = new SoundAnnotation(page, rect, soundPath)
                {
                    // Use the Speaker icon; the annotation remains invisible because the rectangle is zero‑size.
                    Icon = SoundIcon.Speaker
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(sound);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with sound annotations saved to '{outputPath}'.");
    }
}
