using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output_with_sound.pdf";
        const string audioFile  = "background.mp3";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(audioFile))
        {
            Console.Error.WriteLine($"Audio file not found: {audioFile}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position and size on the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a RichMediaAnnotation to embed audio
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // Set the activation event to play when the page becomes visible
            richMedia.ActivateOn = RichMediaAnnotation.ActivationEvent.PageVisible;

            // Specify that the embedded content is audio
            richMedia.Type = RichMediaAnnotation.ContentType.Audio;

            // Load the audio stream and attach it to the annotation
            using (FileStream audioStream = File.OpenRead(audioFile))
            {
                // The MIME type for MP3 audio; adjust if using a different format
                richMedia.SetContent("audio/mpeg", audioStream);
            }

            // Optionally set a contents description for the annotation (Title property does not exist)
            richMedia.Contents = "Automatically plays when the page is visible.";

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with automatic background music saved to '{outputPdf}'.");
    }
}
