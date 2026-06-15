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
        const string soundPath = "tone.wav";

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define a small rectangle for the annotation (position can be adjusted)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 20, 20);

                // Create the SoundAnnotation first
                SoundAnnotation sound = new SoundAnnotation(page, rect, soundPath);
                // Set properties after the object is instantiated
                sound.Icon = SoundIcon.Speaker;
                sound.Color = Aspose.Pdf.Color.Transparent;
                // Border requires the parent annotation instance
                sound.Border = new Border(sound) { Width = 0 };

                // Add the annotation to the page
                page.Annotations.Add(sound);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with sound annotations: {outputPath}");
    }
}
