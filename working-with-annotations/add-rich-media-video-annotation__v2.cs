using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string videoFile = "sample.mp4";        // video to embed
        const string outputPdf = "output_with_richmedia.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(videoFile))
        {
            Console.Error.WriteLine($"Video file not found: {videoFile}");
            return;
        }

        // Load the PDF (lifecycle rule: use Document constructor with file path)
        using (Document doc = new Document(inputPdf))
        {
            // Create a rectangle where the annotation will appear
            var rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Initialize RichMediaAnnotation on the first page
            var richMedia = new RichMediaAnnotation(doc.Pages[1], rect)
            {
                // Set the type to video
                Type = RichMediaAnnotation.ContentType.Video,

                // Activation: Aspose.PDF does not provide a double‑click activation event.
                // The closest supported activation is on page open (or other supported events).
                // Here we use the default (page open) activation. If a specific event is required,
                // use a valid enum value such as RichMediaAnnotation.ActivationEvent.PageOpen.
                // ActivateOn = RichMediaAnnotation.ActivationEvent.PageOpen, // optional

                // Optional: set a description (Contents is defined on the base Annotation class)
                Contents = "Double‑click to play the video. (Note: double‑click activation is not supported; video plays on page open.)"
            };

            // Note: RichMediaAnnotation does not inherit from MarkupAnnotation, so Title cannot be set.
            // If a title is required, it can be stored in the Contents or as a custom property.

            // Embed the video stream
            using (FileStream videoStream = File.OpenRead(videoFile))
            {
                // The first argument is the name of the embedded stream
                richMedia.SetContent(Path.GetFileName(videoFile), videoStream);
            }

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(richMedia);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rich media annotation added and saved to '{outputPdf}'.");
    }
}
