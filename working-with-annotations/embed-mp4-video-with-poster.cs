using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_richmedia.pdf";
        const string videoPath      = "sample.mp4";   // MP4 video to embed
        const string posterImagePath= "poster.jpg";   // Image shown before playback

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }
        if (!File.Exists(posterImagePath))
        {
            Console.Error.WriteLine($"Poster image not found: {posterImagePath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Specify that the embedded content is a video
                Type = RichMediaAnnotation.ContentType.Video,
                // Optional: set a tooltip or description
                Contents = "Embedded MP4 video"
            };

            // Embed the video stream
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                // The first argument is a name for the embedded stream
                richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
            }

            // Set the poster image (displayed before playback)
            using (FileStream posterStream = File.OpenRead(posterImagePath))
            {
                richMedia.SetPoster(posterStream);
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Rich media PDF saved to '{outputPdfPath}'.");
    }
}