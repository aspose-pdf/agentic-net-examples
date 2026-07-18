using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // result PDF
        const string videoPath = "sample.mp4";         // video to embed
        const string posterPath = "poster.jpg";        // optional poster image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // Load the PDF document (using rule for document disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle for the annotation (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Set the type to Video
                Type = RichMediaAnnotation.ContentType.Video,

                // Activate on click (closest to double‑click; Aspose.Pdf supports Click, PageOpen, PageVisible)
                ActivateOn = RichMediaAnnotation.ActivationEvent.Click,

                // Optional: give the annotation a name and tooltip
                Name = "EmbeddedVideo",
                Contents = "Double‑click to play video"
            };

            // Embed the video stream
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
            }

            // Optionally set a poster image (displayed before playback)
            if (File.Exists(posterPath))
            {
                using (FileStream posterStream = File.OpenRead(posterPath))
                {
                    richMedia.SetPoster(posterStream);
                }
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"RichMediaAnnotation added and saved to '{outputPdf}'.");
    }
}