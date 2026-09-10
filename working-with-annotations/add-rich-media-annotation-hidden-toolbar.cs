using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string videoPath   = "sample.mp4";   // video/audio file to embed
        const string outputPath  = "RichMediaAnnotated.pdf";

        // Verify source video exists
        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (left, bottom, width, height)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 300);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Activate on click (you can also use PageOpen or PageVisible)
                ActivateOn = RichMediaAnnotation.ActivationEvent.Click,

                // Hide the default playback toolbar – PDF spec uses flash variables
                // Setting "toolbar=false" removes the toolbar UI
                CustomFlashVariables = "toolbar=false"
            };

            // Embed the video content
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                // The first argument is the name of the embedded stream
                richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
            }

            // Optionally set a poster image (preview shown before playback)
            // using (FileStream poster = File.OpenRead("poster.jpg"))
            // {
            //     richMedia.SetPoster(poster);
            // }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with RichMediaAnnotation saved to '{outputPath}'.");
    }
}