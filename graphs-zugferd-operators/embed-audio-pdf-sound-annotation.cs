using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "audio_embedded.pdf";
        const string audioPath = "sample.wav";

        if (!File.Exists(audioPath))
        {
            Console.Error.WriteLine($"Audio file not found: {audioPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the sound annotation will appear
            Rectangle rect = new Rectangle(100, 500, 150, 550);

            // Create a sound annotation that references the audio file
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, audioPath);

            // Choose an icon for the annotation (Speaker or Mic)
            soundAnn.Icon = SoundIcon.Speaker;

            // Optional: set a title and tooltip text
            soundAnn.Title = "Audio Clip";
            soundAnn.Contents = "Click to play the embedded audio";

            // No explicit JavaScript action is required – clicking the annotation
            // automatically plays the embedded sound in Aspose.Pdf.
            // If custom JavaScript is needed, it can be added at the document level
            // using doc.JavaScript.Add(...), but the SoundAnnotation class does not
            // expose an Action property in the current API version.

            // Attach the annotation to the page
            page.Annotations.Add(soundAnn);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with embedded audio saved to '{outputPath}'.");
    }
}
