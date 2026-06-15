using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";   // source PDF
        const string outputPdf  = "output.pdf";  // result PDF
        const string videoPath  = "sample.mp4";  // MP4 video to embed
        const string posterPath = "poster.jpg";  // poster image shown before playback

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(videoPath) || !File.Exists(posterPath))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Load the source PDF (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // Specify that the embedded content is a video
            richMedia.Type = RichMediaAnnotation.ContentType.Video;

            // Embed the MP4 video stream
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                // The first argument is the name of the embedded stream; any filename works
                richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
            }

            // Set the poster image that appears before playback
            using (FileStream posterStream = File.OpenRead(posterPath))
            {
                richMedia.SetPoster(posterStream);
            }

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(richMedia);

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rich media PDF saved to '{outputPdf}'.");
    }
}