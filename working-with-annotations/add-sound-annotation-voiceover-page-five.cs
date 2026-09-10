using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string soundPath  = "voiceover.wav";

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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least five pages (pages are 1‑based)
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document contains fewer than 5 pages.");
                return;
            }

            // Get page five
            Page page5 = doc.Pages[5];

            // Define the clickable region on the page (left, bottom, width, height)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a SoundAnnotation that plays the specified audio file when clicked
            SoundAnnotation soundAnn = new SoundAnnotation(page5, rect, soundPath)
            {
                Title    = "Voice‑over",
                Contents = "Click to play narration"
                // Optional: Icon = SoundIcon.Speaker; // if you want a speaker icon
            };

            // Add the annotation to the page
            page5.Annotations.Add(soundAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sound annotation added and saved to '{outputPath}'.");
    }
}