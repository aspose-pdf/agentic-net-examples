using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string videoUrl   = "https://example.com/video.mp4";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a ScreenAnnotation.
            // The third parameter is a media file path; we pass an empty string because
            // we will use an external URL via an action.
            ScreenAnnotation screen = new ScreenAnnotation(page, rect, string.Empty);

            // Add an action that opens the external video URL.
            // Use the Actions collection (read‑only Action property cannot be set directly).
            screen.Actions.Add(new GoToURIAction(videoUrl));

            // Optional: set a title and contents for the annotation.
            screen.Title    = "Video Annotation";
            screen.Contents = "Click to play the video";

            // Add the annotation to the page.
            page.Annotations.Add(screen);

            // Save the modified PDF (lifecycle rule: save inside using block).
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ScreenAnnotation to '{outputPath}'.");
    }
}