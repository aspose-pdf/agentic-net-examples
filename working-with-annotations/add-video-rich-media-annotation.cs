using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "richmedia.pdf";
        const string videoPath = "sample.mp4";
        const string posterPath = "poster.jpg";

        if (!File.Exists(videoPath) || !File.Exists(posterPath))
        {
            Console.Error.WriteLine("Video or poster image file not found.");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Initialize RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Specify that the embedded content is a video
                Type = RichMediaAnnotation.ContentType.Video
            };

            // Embed the MP4 video stream
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                // The first argument is the name of the embedded stream
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
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rich media PDF saved to '{outputPath}'.");
    }
}