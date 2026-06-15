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
        const string soundPath  = "voiceover.wav";

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

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 5 exists (pages are 1‑based)
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document has fewer than 5 pages.");
                return;
            }

            Page page5 = doc.Pages[5];

            // Define the clickable region (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the SoundAnnotation (constructor: SoundAnnotation(Page, Rectangle, string))
            SoundAnnotation soundAnn = new SoundAnnotation(page5, rect, soundPath)
            {
                Title    = "Narration",
                Contents = "Click to play the voice‑over."
                // Optional: Icon = SoundIcon.Speaker; // if desired and supported
            };

            // Add the annotation to the page (lifecycle: create)
            page5.Annotations.Add(soundAnn);

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with sound annotation saved to '{outputPath}'.");
    }
}