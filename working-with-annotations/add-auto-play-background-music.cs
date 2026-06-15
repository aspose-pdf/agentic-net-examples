using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "output_with_audio.pdf"; // result PDF
        const string audioFile  = "background.mp3";     // audio to play

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(audioFile))
        {
            Console.Error.WriteLine($"Audio file not found: {audioFile}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position and size on the page)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a RichMediaAnnotation – it supports activation events such as PageVisible
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // Set the activation event so the audio starts automatically when the page becomes visible
            richMedia.ActivateOn = RichMediaAnnotation.ActivationEvent.PageVisible;

            // Specify that the embedded content is audio
            richMedia.Type = RichMediaAnnotation.ContentType.Audio;

            // Embed the audio file into the annotation
            using (FileStream audioStream = File.OpenRead(audioFile))
            {
                // MIME type for MP3 audio; adjust if using a different format
                richMedia.SetContent("audio/mpeg", audioStream);
            }

            // Optionally set an icon to indicate audio (Speaker icon)
            // richMedia.Icon = SoundIcon.Speaker; // Not applicable to RichMediaAnnotation; kept as comment

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with auto‑play audio annotation: {outputPdf}");
    }
}