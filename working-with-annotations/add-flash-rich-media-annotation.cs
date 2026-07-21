using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to resources – adjust as needed
        const string outputPdf = "RichMediaFlash.pdf";
        const string flashVideoPath = "sample_video.swf";   // Flash video (SWF)
        const string flashPlayerPath = "custom_player.swf"; // Optional custom player
        const string posterImagePath = "poster.jpg";        // Poster image shown before playback

        // Verify that required files exist
        if (!File.Exists(flashVideoPath))
        {
            Console.Error.WriteLine($"Video file not found: {flashVideoPath}");
            return;
        }
        if (!File.Exists(flashPlayerPath))
        {
            Console.Error.WriteLine($"Player file not found: {flashPlayerPath}");
            return;
        }
        if (!File.Exists(posterImagePath))
        {
            Console.Error.WriteLine($"Poster image not found: {posterImagePath}");
            return;
        }

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the RichMedia annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Initialize the RichMediaAnnotation on page 1
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Activate the media on mouse click
                ActivateOn = RichMediaAnnotation.ActivationEvent.Click,

                // Optional: pass flash variables (e.g., autoplay and loop)
                CustomFlashVariables = "autoplay=true;loop=true;",

                // Optional: set a custom flash player (if you have one)
                // The property expects a Stream; we will assign it later
            };

            // Set the custom player stream
            using (FileStream playerStream = File.OpenRead(flashPlayerPath))
            {
                richMedia.CustomPlayer = playerStream;
                // Note: the stream is consumed by the annotation; keep it open until Save()
                // (Aspose.Pdf copies the stream internally, so we can close it afterwards)
            }

            // Set the poster image (displayed before playback starts)
            using (FileStream posterStream = File.OpenRead(posterImagePath))
            {
                richMedia.SetPoster(posterStream);
            }

            // Embed the Flash video content
            using (FileStream videoStream = File.OpenRead(flashVideoPath))
            {
                // The first argument is the name of the embedded stream
                richMedia.SetContent(Path.GetFileName(flashVideoPath), videoStream);
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rich media PDF created: {outputPdf}");
    }
}