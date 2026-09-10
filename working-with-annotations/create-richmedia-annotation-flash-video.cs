using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to source files (ensure they exist)
        const string outputPdf = "RichMediaAnnotation.pdf";
        const string flashVideoPath = "sample.swf";      // Flash video file
        const string flashPlayerPath = "player.swf";     // Optional custom player

        // Create a new PDF document with a single blank page
        using (Document doc = new Document())
        {
            // Add a blank page (page index will be 1)
            doc.Pages.Add();

            // Get the first page
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear
            // (left, bottom, right, top) in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation on page one
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // Set the type of content to Video using the enum value
            richMedia.Type = RichMediaAnnotation.ContentType.Video;

            // Activate the annotation on mouse click
            richMedia.ActivateOn = RichMediaAnnotation.ActivationEvent.Click;

            // Optional: set custom flash variables (e.g., autoplay and loop)
            richMedia.CustomFlashVariables = "autoplay=true;loop=true";

            // Optional: embed a custom flash player (if you have one)
            if (File.Exists(flashPlayerPath))
            {
                using (FileStream playerStream = File.OpenRead(flashPlayerPath))
                {
                    richMedia.CustomPlayer = playerStream;
                }
            }

            // Embed the Flash video content
            if (File.Exists(flashVideoPath))
            {
                using (FileStream videoStream = File.OpenRead(flashVideoPath))
                {
                    // The first argument is the name of the stream inside the PDF
                    richMedia.SetContent(Path.GetFileName(flashVideoPath), videoStream);
                }
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with RichMediaAnnotation saved to '{outputPdf}'.");
    }
}
