using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string videoPath = "sample.mp4"; // video file to embed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (coordinates in points)
            Rectangle rect = new Rectangle(100, 500, 400, 800);

            // Create the ScreenAnnotation – the media file is supplied in the constructor
            ScreenAnnotation screenAnno = new ScreenAnnotation(page, rect, videoPath);

            // NOTE: In the current Aspose.Pdf version the ScreenAnnotation does not expose
            // properties for playback mode (loop, autoplay) or UI controls. The annotation
            // will autoplay when opened in a PDF viewer that supports media playback.
            // If fine‑grained control is required, low‑level PDF dictionary manipulation
            // would be needed, which is beyond the scope of the core API.

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(screenAnno);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Screen annotation with video saved to '{outputPath}'.");
    }
}
