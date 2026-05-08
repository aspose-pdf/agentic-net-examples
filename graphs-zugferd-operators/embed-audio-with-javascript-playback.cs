using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class EmbedAudioWithJs
{
    static void Main()
    {
        const string outputPdf = "AudioWithControls.pdf";
        const string audioFile = "sample.mp3";

        // Ensure the audio file exists – create a minimal silent MP3 if it does not.
        if (!File.Exists(audioFile))
        {
            // A very small MP3 header (silence) – sufficient for embedding.
            byte[] silentMp3 = new byte[]
            {
                0x49,0x44,0x33,0x03,0x00,0x00,0x00,0x00,0x00,0x21,0xFF,0xFB,0x90,0x64,0x00,0x00,
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            };
            File.WriteAllBytes(audioFile, silentMp3);
        }

        using (Document doc = new Document())
        {
            // Add a blank page.
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (position and size).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a SoundAnnotation that references the audio file.
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, audioFile)
            {
                Name = "audio1",
                Color = Aspose.Pdf.Color.Blue
            };

            // JavaScript that plays the sound when the page opens.
            JavascriptAction jsAction = new JavascriptAction("this.getAnnots()[0].play();");
            page.Actions.OnOpen = jsAction;

            // Add the annotation to the page.
            page.Annotations.Add(soundAnn);

            // Save the PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with embedded audio saved to '{outputPdf}'.");
    }
}
