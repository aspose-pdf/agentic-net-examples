using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and background music file paths
        const string inputPdf = "input.pdf";
        const string musicFile = "background.mp3";
        const string outputPdf = "output_with_sound.pdf";

        // Ensure the source files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(musicFile))
        {
            Console.Error.WriteLine($"Audio file not found: {musicFile}");
            return;
        }

        // Load the PDF document (using the standard load pattern)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define an (invisible) rectangle for the annotation.
            // Using a zero‑size rectangle keeps the annotation out of view.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create a RichMediaAnnotation – it can embed audio and be set to auto‑play.
            RichMediaAnnotation audioAnno = new RichMediaAnnotation(page, rect);

            // Activate the annotation when the page becomes visible.
            audioAnno.ActivateOn = RichMediaAnnotation.ActivationEvent.PageVisible;

            // Specify that the embedded media is audio.
            audioAnno.Type = RichMediaAnnotation.ContentType.Audio;

            // Load the audio file and attach it to the annotation.
            using (FileStream audioStream = File.OpenRead(musicFile))
            {
                // The first argument is a name for the media; it can be any string.
                audioAnno.SetContent("background.mp3", audioStream);
            }

            // Optional: make the annotation completely invisible (no border, no icon).
            // Border requires the parent annotation in its constructor.
            audioAnno.Border = new Border(audioAnno) { Width = 0 };

            // Add the annotation to the page.
            page.Annotations.Add(audioAnno);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with background music: {outputPdf}");
    }
}