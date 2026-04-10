using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";
        const string videoPath  = "sample.mp4";

        // Verify that the video file exists before creating the annotation
        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // Document lifecycle must be wrapped in a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the screen annotation will appear
            // Rectangle(left, bottom, right, top) – fully qualified to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the ScreenAnnotation with the video file path
            ScreenAnnotation screen = new ScreenAnnotation(page, rect, videoPath);

            // Optional: set a title and contents for the annotation
            screen.Title    = "Embedded Video";
            screen.Contents = "Video plays in loop with controls disabled";

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(screen);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with ScreenAnnotation saved to '{outputPath}'.");
    }
}