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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least five pages (1‑based indexing)
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document contains fewer than 5 pages.");
                return;
            }

            // Define the clickable region on page 5 (coordinates are in points)
            // Example: lower‑left (100, 500), upper‑right (200, 600)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a SoundAnnotation that plays the specified audio file when clicked
            SoundAnnotation soundAnn = new SoundAnnotation(doc.Pages[5], rect, soundPath)
            {
                // Optional: set an icon to indicate audio content
                Icon = SoundIcon.Speaker,
                // Optional: tooltip text shown on hover
                Contents = "Play voice‑over narration",
                Title    = "Audio Annotation"
            };

            // Add the annotation to page 5
            doc.Pages[5].Annotations.Add(soundAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with sound annotation: {outputPath}");
    }
}