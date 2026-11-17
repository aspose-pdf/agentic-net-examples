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
        const string soundPath  = "chime.wav";
        const int   targetPage = 3; // page on which the sound should play

        // Validate input files
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
            // Ensure the requested page exists (Aspose.Pdf uses 1‑based indexing)
            if (targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPage} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Retrieve the target page
            Page page = doc.Pages[targetPage];

            // Define the annotation rectangle (coordinates are in points)
            Rectangle rect = new Rectangle(100, 500, 200, 550);

            // Create a SoundAnnotation attached to the page.
            // The constructor signature is (Page, Rectangle, string soundFilePath).
            // The annotation will be displayed with the default speaker icon and will play when the user clicks it.
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundPath)
            {
                Name = "PageChime",
                Icon = SoundIcon.Speaker,
                Contents = "Click to play chime"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(soundAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with sound annotation saved to '{outputPath}'.");
    }
}
