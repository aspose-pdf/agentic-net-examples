using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static async Task Main(string[] args)
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string videoUrl = "https://example.com/video.mp4";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Download the video stream from the online URL.
        using (HttpClient httpClient = new HttpClient())
        using (Stream videoStream = await httpClient.GetStreamAsync(videoUrl))
        // Open the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (1‑based indexing).
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the RichMediaAnnotation.
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Specify that the content is a video.
                Type = RichMediaAnnotation.ContentType.Video,
                // Make the annotation read‑only to prevent downloading.
                Flags = Aspose.Pdf.Annotations.AnnotationFlags.ReadOnly,
                // Optional: make the annotation background transparent.
                Color = Aspose.Pdf.Color.Transparent
            };

            // Attach the video content to the annotation.
            // The first argument is an arbitrary name for the stream.
            richMedia.SetContent("video.mp4", videoStream);

            // Add the annotation to the page.
            page.Annotations.Add(richMedia);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rich media annotation added and saved to '{outputPath}'.");
    }
}