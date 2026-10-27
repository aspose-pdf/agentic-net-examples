using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string audioPath = "sample.mp3";
        const string outputPdf = "audio_embedded.pdf";

        if (!File.Exists(audioPath))
        {
            Console.Error.WriteLine($"Audio file not found: {audioPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Rectangle for the Play button
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a button field that will trigger JavaScript playback
            ButtonField playButton = new ButtonField(page, btnRect);
            playButton.PartialName = "PlayButton";               // internal name
            playButton.AlternateCaption = "Play Audio";          // visible label
            // JavaScript: play the first annotation on the page (the sound annotation added below)
            playButton.Actions.OnPressMouseBtn = new JavascriptAction("this.getAnnots()[0].play();");

            // Add the button to the document's form collection
            doc.Form.Add(playButton);

            // Invisible sound annotation that holds the audio stream
            Aspose.Pdf.Rectangle soundRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0); // zero‑size rectangle makes it invisible
            SoundAnnotation sound = new SoundAnnotation(page, soundRect, audioPath);
            sound.Name = "EmbeddedAudio";
            // Hide the annotation from the viewer (optional)
            sound.Flags = AnnotationFlags.Hidden;
            page.Annotations.Add(sound);

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with embedded audio saved to '{outputPdf}'.");
    }
}
