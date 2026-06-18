using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF (must exist)
        const string outputPdf  = "output_richmedia.pdf";
        const string flashPlayerPath = "player.swf";    // custom Flash player (SWF)
        const string videoPath      = "video.flv";    // Flash video file
        const string posterPath     = "poster.jpg";   // optional poster image

        if (!File.Exists(inputPdf) ||
            !File.Exists(flashPlayerPath) ||
            !File.Exists(videoPath))
        {
            Console.Error.WriteLine("Required file(s) not found.");
            return;
        }

        // Load the PDF, modify, and save – all within a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Page 1 (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 500, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Activate on mouse click
                ActivateOn = RichMediaAnnotation.ActivationEvent.Click,
                // Specify that the embedded content is a video
                Type = RichMediaAnnotation.ContentType.Video,
                // Pass flash variables (e.g., autoplay, loop)
                CustomFlashVariables = "autoplay=true&loop=false"
            };

            // Embed the custom Flash player (SWF) – the player stream is kept open only while setting
            using (FileStream playerStream = File.OpenRead(flashPlayerPath))
            {
                richMedia.CustomPlayer = playerStream;
                // Note: the stream is copied internally, so it can be closed after assignment
            }

            // Embed the video content
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                // The first argument is the name of the embedded stream
                richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
            }

            // Optional: set a poster image that is shown before playback starts
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