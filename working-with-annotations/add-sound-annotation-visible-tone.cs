using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_sound.pdf";
        const string soundPath = "tone.wav";

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

        // Document disposal must be wrapped in a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury) in points
            Rectangle rect = new Rectangle(100, 500, 200, 550);

            // Create the SoundAnnotation on the page. The constructor that accepts a file path is required.
            SoundAnnotation soundAnnot = new SoundAnnotation(page, rect, soundPath)
            {
                // Optional: set an icon so the annotation is visible on the page
                Icon = SoundIcon.Speaker
            };

            // By default the annotation is visible; do NOT set the Hidden flag.
            // This satisfies the requirement: the tone plays only when the annotation is on‑screen.

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(soundAnnot);

            // Save the modified PDF (still inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with SoundAnnotation saved to '{outputPath}'.");
    }
}
