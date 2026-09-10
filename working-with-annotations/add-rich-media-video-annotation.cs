using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string videoPath = "sample.mp4";
        const string posterPath = "poster.jpg";

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(videoPath) || !File.Exists(posterPath))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Open the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Select the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left X/Y, upper‑right X/Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Optional visual settings
                Color = Aspose.Pdf.Color.Transparent,
                // Specify that the embedded content is a video
                Type = RichMediaAnnotation.ContentType.Video
            };

            // Embed the MP4 video stream
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                // The first argument is the name of the stream inside the PDF
                richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
            }

            // Set the poster image that appears before playback
            using (FileStream posterStream = File.OpenRead(posterPath))
            {
                richMedia.SetPoster(posterStream);
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rich media annotation added and saved to '{outputPdf}'.");
    }
}