using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the media files – replace with actual file locations
        const string videoPath   = "video.flv";   // Flash video file
        const string playerPath  = "player.swf"; // Custom Flash player (SWF)
        const string outputPath  = "RichMediaAnnotation.pdf";

        // Ensure the source files exist
        if (!File.Exists(videoPath) || !File.Exists(playerPath))
        {
            Console.Error.WriteLine("Video or player file not found.");
            return;
        }

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the annotation will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation on page 1
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Activate the media when the annotation is clicked
                ActivateOn = RichMediaAnnotation.ActivationEvent.Click,

                // Pass Flash variables (e.g., autoplay)
                CustomFlashVariables = "autoplay=true"
            };

            // Set the custom Flash player (SWF) stream
            using (FileStream playerStream = File.OpenRead(playerPath))
            {
                richMedia.CustomPlayer = playerStream;
            }

            // Set the video content stream
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                // The first argument is the name of the stream inside the PDF
                richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
            }

            // Optionally set a poster image (first frame preview)
            // using (FileStream posterStream = File.OpenRead("poster.jpg"))
            // {
            //     richMedia.SetPoster(posterStream);
            // }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with RichMediaAnnotation saved to '{outputPath}'.");
    }
}