using System;
using System.IO;
using System.Net.Http;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF (must exist)
        const string outputPdf = "output.pdf";     // result PDF
        const string videoUrl  = "https://example.com/video.mp4"; // online video

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the existing PDF inside a using block (ensures deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle for the RichMediaAnnotation (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Activate on click
                ActivateOn = RichMediaAnnotation.ActivationEvent.Click,
                // Specify that the content is a video
                Type = RichMediaAnnotation.ContentType.Video,
                // Prevent user from downloading the media (read‑only annotation)
                Flags = AnnotationFlags.ReadOnly,
                // Optional: give the annotation a name
                Name = "OnlineVideo"
            };

            // Download the video stream from the URL and attach it to the annotation
            using (HttpClient http = new HttpClient())
            using (Stream videoStream = http.GetStreamAsync(videoUrl).Result)
            {
                // The first parameter is a logical name for the stream; it can be any string
                richMedia.SetContent("video.mp4", videoStream);
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"RichMediaAnnotation added and saved to '{outputPdf}'.");
    }
}