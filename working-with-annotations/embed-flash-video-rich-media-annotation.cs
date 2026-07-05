using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "RichMedia.pdf";
        const string flashPath  = "sample.swf";   // Path to the Flash video (SWF)
        const string posterPath = "poster.jpg";   // Optional poster image

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (page 1)
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Initialize RichMediaAnnotation on page 1
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // Activate the media on mouse click
            richMedia.ActivateOn = RichMediaAnnotation.ActivationEvent.Click;

            // Set flash variables (e.g., autoplay and loop)
            richMedia.CustomFlashVariables = "autoplay=true;loop=true";

            // OPTIONAL: set a custom flash player (another SWF file)
            // using (FileStream playerStream = File.OpenRead("customPlayer.swf"))
            // {
            //     richMedia.CustomPlayer = playerStream;
            // }

            // Embed the Flash video content if the file exists
            if (File.Exists(flashPath))
            {
                using (FileStream videoStream = File.OpenRead(flashPath))
                {
                    // The first argument is the internal name of the stream
                    richMedia.SetContent("video.swf", videoStream);
                }
            }
            else
            {
                Console.WriteLine($"Warning: Flash video file '{flashPath}' not found. Rich media annotation will be added without embedded content.");
            }

            // OPTIONAL: set a poster image displayed before playback
            if (File.Exists(posterPath))
            {
                using (FileStream posterStream = File.OpenRead(posterPath))
                {
                    richMedia.SetPoster(posterStream);
                }
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rich media PDF saved to '{outputPath}'.");
    }
}
