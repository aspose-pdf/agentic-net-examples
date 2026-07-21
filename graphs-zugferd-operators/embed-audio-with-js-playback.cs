using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPath = "AudioWithJs.pdf";
        const string audioFile   = "sample.wav"; // must be a supported audio format

        // Ensure the audio file exists
        if (!File.Exists(audioFile))
        {
            Console.Error.WriteLine($"Audio file not found: {audioFile}");
            return;
        }

        // Create a new PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the sound annotation will appear
            Aspose.Pdf.Rectangle soundRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a SoundAnnotation that embeds the audio file
            // Constructor: SoundAnnotation(Page, Rectangle, string soundFile)
            SoundAnnotation soundAnn = new SoundAnnotation(page, soundRect, audioFile);
            // Optional: set a tooltip
            soundAnn.Contents = "Click to play audio";
            // Visual cue – set the annotation's border color
            soundAnn.Color = Aspose.Pdf.Color.LightGray;

            // Add the sound annotation to the page
            page.Annotations.Add(soundAnn);

            // ------------------------------------------------------------
            // Add a button field that will control playback via JavaScript
            // ------------------------------------------------------------
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(350, 500, 450, 550);

            // Create a ButtonField (AcroForm button)
            ButtonField playButton = new ButtonField(page, btnRect);
            playButton.PartialName = "PlayAudioBtn";
            playButton.Value = "Play Audio";
            playButton.Color = Aspose.Pdf.Color.Yellow;

            // JavaScript to play the first annotation (the SoundAnnotation)
            string jsCode = @"
                var annots = this.getAnnots();
                if (annots != null && annots.length > 0) {
                    // Assuming the first annotation is the sound annotation
                    annots[0].play();
                }";

            // Attach the JavaScript action to the button's mouse‑up event using a supported property
            // Valid action properties for ButtonField are in the AnnotationActionCollection (e.g., OnPressMouseBtn, OnReleaseMouseBtn)
            playButton.Actions.OnReleaseMouseBtn = new JavascriptAction(jsCode);

            // Add the button field to the document's form collection
            doc.Form.Add(playButton, 1);

            // Save the PDF (using the Document.Save(string) overload)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created: {outputPath}");
    }
}
