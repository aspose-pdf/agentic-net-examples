using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.LogicalStructure; // not needed but included for completeness

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string audioPath  = "background.mp3"; // supported audio format (e.g., .wav, .mp3)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(audioPath))
        {
            Console.Error.WriteLine($"Audio file not found: {audioPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle area for the annotation (in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a RichMediaAnnotation (audio type) that will play automatically when the page becomes visible
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // Set the activation event to PageVisible (auto‑play when the page is shown)
            richMedia.ActivateOn = RichMediaAnnotation.ActivationEvent.PageVisible;

            // Specify that the embedded media is audio
            richMedia.Type = RichMediaAnnotation.ContentType.Audio;

            // Load the audio file into a stream and attach it to the annotation
            using (FileStream audioStream = File.OpenRead(audioPath))
            {
                // The SetContent method expects a name for the media and the stream containing the data
                richMedia.SetContent("backgroundAudio", audioStream);
            }

            // Optionally set an icon so the annotation is visible in the PDF viewer
            // (SoundIcon.Speaker is a common choice for audio)
            // Note: RichMediaAnnotation does not have an Icon property; if visual cue is needed,
            // a separate ScreenAnnotation or a visible rectangle can be added.
            // Here we simply add the RichMediaAnnotation to the page.
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with auto‑play background audio saved to '{outputPath}'.");
    }
}