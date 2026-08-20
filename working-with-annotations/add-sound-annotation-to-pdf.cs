using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_with_sound.pdf"; // result PDF
        const string soundFile  = "audio.mp3";          // MP3 to play

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(soundFile))
        {
            Console.Error.WriteLine($"Sound file not found: {soundFile}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (coordinates are in points; lower‑left origin)
            // Fully qualify the Rectangle type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the SoundAnnotation using the constructor that takes the page, rectangle and sound file path
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundFile)
            {
                // Optional: set an icon (Speaker or Mic) to indicate a sound annotation
                Icon = SoundIcon.Speaker,
                // Optional: set a title and contents that appear in the popup window
                Title    = "Audio Note",
                Contents = "Click to play the attached MP3."
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(soundAnn);

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with sound annotation saved to '{outputPath}'.");
    }
}