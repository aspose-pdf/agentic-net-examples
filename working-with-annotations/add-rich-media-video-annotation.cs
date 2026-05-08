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
        const string posterPath = "poster.jpg";     // Image shown before playback

        // Ensure source files exist
        if (!File.Exists(videoPath) || !File.Exists(posterPath))
        {
            Console.Error.WriteLine("Video or poster image file not found.");
            return;
        }

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create RichMediaAnnotation on the page
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

            // Set the poster image that appears before playback
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