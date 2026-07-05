using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "audio_embedded.pdf";
        const string audioPath  = "sample.mp3";

        if (!File.Exists(audioPath))
        {
            Console.Error.WriteLine($"Audio file not found: {audioPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (position and size on the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a RichMediaAnnotation for audio content
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);
            richMedia.Type = RichMediaAnnotation.ContentType.Audio;          // Specify that the media is audio
            // Activation event "MouseDown" is not available in the current Aspose.Pdf version.
            // If you need automatic playback, you can use a supported event such as PageOpen.
            // richMedia.ActivateOn = RichMediaAnnotation.ActivationEvent.PageOpen;

            // Embed the audio file into the annotation
            using (FileStream audioStream = File.OpenRead(audioPath))
            {
                // The first parameter is an arbitrary name for the stream
                richMedia.SetContent("audio.mp3", audioStream);
            }

            // Add a JavaScript action to control playback (e.g., play when clicked)
            // The script accesses the first annotation on the page and calls its play method
            JavascriptAction jsAction = new JavascriptAction("this.getAnnots()[0].play();");
            richMedia.Actions.Add(jsAction);

            // Attach the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with embedded audio saved to '{outputPath}'.");
    }
}
