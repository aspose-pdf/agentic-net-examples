using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output_with_sound.pdf"; // result PDF
        const string soundFile = "background.mp3";    // audio to play

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(soundFile))
        {
            Console.Error.WriteLine($"Sound file not found: {soundFile}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a RichMediaAnnotation (audio) that will be triggered when the page becomes visible
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Set the activation event to PageVisible (auto‑play on visibility)
                ActivateOn = RichMediaAnnotation.ActivationEvent.PageVisible,
                // Specify that the embedded media is audio
                Type = RichMediaAnnotation.ContentType.Audio
            };

            // Embed the sound file into the annotation
            using (FileStream audioStream = File.OpenRead(soundFile))
            {
                // MIME type for MP3 audio
                richMedia.SetContent("audio/mpeg", audioStream);
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with auto‑play sound saved to '{outputPdf}'.");
    }
}
