using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for Color

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string soundPath = "sound.wav"; // audio file to embed

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a page 2.");
                return;
            }

            // Get page 2 (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[2];

            // Define the annotation rectangle (lower‑left X/Y, upper‑right X/Y)
            var rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create the SoundAnnotation – the sound file is supplied via the constructor
            var soundAnno = new SoundAnnotation(page, rect, soundPath)
            {
                // Set a custom color (RGB values are 0‑1)
                Color = Color.FromRgb(0.2, 0.5, 0.8),

                // Title displayed in the annotation UI
                Title = "Sample Sound",

                // Optional contents (tooltip)
                Contents = "Click to play the embedded sound."
            };

            // Create a PopupAnnotation that will appear when the sound annotation is activated
            var popup = new PopupAnnotation(page, rect)
            {
                // Text shown inside the popup window
                Contents = "This is a popup for the sound annotation."
                // Title property is not available in the current Aspose.Pdf version, so it is omitted.
            };

            // Associate the popup with the sound annotation
            soundAnno.Popup = popup;

            // Add both annotations to the page
            page.Annotations.Add(soundAnno);
            page.Annotations.Add(popup);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with SoundAnnotation saved to '{outputPath}'.");
    }
}