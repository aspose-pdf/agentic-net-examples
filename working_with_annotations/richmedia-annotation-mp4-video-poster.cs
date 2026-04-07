using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPdf = "RichMediaVideo.pdf";
        const string videoPath = "sample.mp4";      // MP4 video file
        const string posterPath = "poster.jpg";     // Image displayed before playback

        // Ensure source files exist
        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }
        if (!File.Exists(posterPath))
        {
            Console.Error.WriteLine($"Poster image not found: {posterPath}");
            return;
        }

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the annotation will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Specify that the embedded content is a video
                Type = RichMediaAnnotation.ContentType.Video
            };

            // Embed the MP4 video stream
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
            }

            // Set the poster image (displayed before playback)
            using (FileStream posterStream = File.OpenRead(posterPath))
            {
                richMedia.SetPoster(posterStream);
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with RichMediaAnnotation saved to '{outputPdf}'.");
    }
}