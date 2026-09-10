using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // RichMediaAnnotation resides here

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string videoPath = "sample.mp4";      // video to embed
        const string outputPath = "RichMediaAutoPlay.pdf";

        // Ensure the video file exists
        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation on the page
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Set the media type – use the enum, not a string
                Type = RichMediaAnnotation.ContentType.Video,

                // Activate automatically when the page becomes visible
                ActivateOn = RichMediaAnnotation.ActivationEvent.PageVisible
            };

            // Embed the video stream into the annotation
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                // The first argument is the name of the stream inside the PDF
                richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
            }

            // Optionally set a poster image (preview) – omitted here

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with auto‑playing RichMediaAnnotation saved to '{outputPath}'.");
    }
}
