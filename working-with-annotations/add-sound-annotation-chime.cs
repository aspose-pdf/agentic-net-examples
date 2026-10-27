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
        const string soundPath  = "chime.wav";      // sound file to play
        const int    targetPage = 3;                // page number where sound should play (1‑based)

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
            // Validate page number
            if (targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPage} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Get the target page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[targetPage];

            // Define an invisible rectangle where the annotation will be placed.
            // The rectangle can be zero‑size because we only need the annotation to trigger.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create the SoundAnnotation using the constructor that accepts the audio file path.
            SoundAnnotation soundAnno = new SoundAnnotation(page, rect, soundPath);

            // Optionally give the annotation a name (useful for debugging)
            soundAnno.Name = "PageScrollChime";

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(soundAnno);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sound annotation added. Output saved to '{outputPath}'.");
    }
}
