using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input multimedia file (ensure the file exists at the specified path)
        const string mediaPath = "sample.mp4";          // video file (MP4)
        const string posterPath = "poster.jpg";        // optional poster image
        const string outputPdf = "multimedia_annotation.pdf";

        if (!File.Exists(mediaPath))
        {
            Console.Error.WriteLine($"Media file not found: {mediaPath}");
            return;
        }

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the annotation will appear (coordinates in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Set the type of content – use the enum, not a string
                Type = RichMediaAnnotation.ContentType.Video,

                // Optional: set a description that appears in the annotation popup
                // The base Annotation class provides the "Contents" property.
                Contents = "Click to play the embedded video."
            };

            // Embed the video content
            using (FileStream mediaStream = File.OpenRead(mediaPath))
            {
                // The first argument is a MIME type identifier (e.g., "video/mp4")
                richMedia.SetContent("video/mp4", mediaStream);
            }

            // Optionally embed a poster image that is shown before playback
            if (File.Exists(posterPath))
            {
                using (FileStream posterStream = File.OpenRead(posterPath))
                {
                    // SetPoster also expects a MIME type; Aspose.Pdf infers it from the stream if omitted.
                    richMedia.SetPoster(posterStream);
                }
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the PDF (wrapped in using ensures proper disposal)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with multimedia annotation saved to '{outputPdf}'.");
    }
}
