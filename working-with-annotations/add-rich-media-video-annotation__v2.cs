using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string videoPath = "sample.mp4"; // path to the video file to embed

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // Load the PDF document (using the recommended lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle for the RichMediaAnnotation (coordinates are in points)
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Set the activation event to Click (single click). 
                // Aspose.Pdf does not expose a double‑click event; Click is the closest option.
                ActivateOn = RichMediaAnnotation.ActivationEvent.Click,

                // Specify that the content type is video
                Type = RichMediaAnnotation.ContentType.Video
            };

            // Embed the video file into the annotation
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                // The first parameter is the name of the embedded stream; it can be any identifier.
                richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
            }

            // Optionally set a poster image (preview) for the video.
            // If you have a poster image, uncomment the following lines and provide the image path.
            // const string posterPath = "poster.jpg";
            // if (File.Exists(posterPath))
            // {
            //     using (FileStream posterStream = File.OpenRead(posterPath))
            //     {
            //         richMedia.SetPoster(posterStream);
            //     }
            // }

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(richMedia);

            // Save the modified PDF (standard PDF save, no extra SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rich media annotation added and saved to '{outputPdf}'.");
    }
}