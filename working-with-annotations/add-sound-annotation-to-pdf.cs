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
        const string mp3Path    = "sound.mp3";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(mp3Path))
        {
            Console.Error.WriteLine($"MP3 file not found: {mp3Path}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left X/Y, upper‑right X/Y).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a SoundAnnotation that references the MP3 file.
            // The constructor (Page, Rectangle, string) attaches the annotation to the page.
            SoundAnnotation soundAnno = new SoundAnnotation(page, rect, mp3Path);

            // Optional: set a visible icon and a title for the annotation.
            soundAnno.Icon  = SoundIcon.Speaker;   // uses a built‑in speaker icon
            soundAnno.Title = "Play Audio";

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(soundAnno);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with sound annotation saved to '{outputPath}'.");
    }
}