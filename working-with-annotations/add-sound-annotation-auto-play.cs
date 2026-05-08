using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";               // source PDF
        const string outputPdf  = "output_with_sound.pdf";   // result PDF
        const string soundFile  = "notify.wav";              // notification tone (WAV, MP3, etc.)

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

        // Load the PDF document (use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the SoundAnnotation – constructor (Page, Rectangle, soundFilePath)
            // The SoundAnnotation itself contains the sound data and will play the sound
            // when the annotation becomes visible (e.g., when the page is displayed).
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundFile)
            {
                // Choose an icon that will be displayed on the page
                Icon = SoundIcon.Speaker,

                // Optional: give the annotation a title and contents (shown in the popup)
                Title    = "Notification",
                Contents = "Play notification tone when visible."
            };

            // NOTE: In recent Aspose.Pdf versions the SoundAnnotation plays the sound
            // automatically when the annotation is rendered (i.e., becomes visible).
            // The older "OnMouseEnter" action is no longer part of the public API, so we
            // rely on the built‑in behaviour of SoundAnnotation.

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(soundAnn);

            // Save the modified document (lifecycle rule: save inside the using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with sound annotation saved to '{outputPdf}'.");
    }
}
