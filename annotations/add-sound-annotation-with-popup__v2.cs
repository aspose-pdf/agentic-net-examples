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
        const string soundFilePath = "background.wav"; // optional sound file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 2 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a page 2.");
                return;
            }

            // Retrieve page 2
            Page page = doc.Pages[2];

            // Rectangle for the sound annotation icon
            Rectangle annotRect = new Rectangle(100, 500, 200, 550);

            // Create the SoundAnnotation on page 2. The constructor that accepts a file path must be used.
            SoundAnnotation sound = new SoundAnnotation(page, annotRect, File.Exists(soundFilePath) ? soundFilePath : null)
            {
                Title = "Background Music",
                // Optional: choose an icon (Speaker or Mic). Default is Speaker.
                Icon = SoundIcon.Speaker,
                // Optional: set a visible label when the annotation is opened.
                Contents = "Play background music"
            };

            // Define the popup rectangle (position of the popup window)
            Rectangle popupRect = new Rectangle(250, 600, 450, 750);
            sound.Popup = new PopupAnnotation(page, popupRect);

            // Add the annotation to the page
            page.Annotations.Add(sound);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with SoundAnnotation to '{outputPath}'.");
    }
}
