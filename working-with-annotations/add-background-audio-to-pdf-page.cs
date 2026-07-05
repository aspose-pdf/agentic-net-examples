using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string audioFile = "background.mp3";

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

        // Load the existing PDF
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top)
            var rect = new Rectangle(100, 500, 200, 550);

            // Create a RichMediaAnnotation that will play audio automatically when the page becomes visible
            var richMedia = new RichMediaAnnotation(page, rect)
            {
                ActivateOn = RichMediaAnnotation.ActivationEvent.PageVisible // play on page visible
            };

            // Attach the audio stream to the annotation. The MIME type is supplied via SetContent;
            // there is no separate ContentType property to set.
            using (FileStream audioStream = File.OpenRead(audioFile))
            {
                richMedia.SetContent("audio/mpeg", audioStream);
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with background audio: {outputPdf}");
    }
}