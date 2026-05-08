using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string soundPath = "voiceover.wav";

        // Verify required files exist
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

        // Load the PDF document (using the recommended lifecycle pattern)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least five pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document contains fewer than 5 pages.");
                return;
            }

            // Get page five
            Page page = doc.Pages[5];

            // Define the clickable region for the annotation:
            // left = 100, bottom = 500, width = 100, height = 50
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a SoundAnnotation that plays the specified audio file when clicked
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundPath)
            {
                // Optional visual cues
                Icon = SoundIcon.Speaker,
                Title = "Voice‑over",
                Contents = "Click to play narration."
            };

            // Attach the annotation to the page
            page.Annotations.Add(soundAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sound annotation added and saved to '{outputPath}'.");
    }
}