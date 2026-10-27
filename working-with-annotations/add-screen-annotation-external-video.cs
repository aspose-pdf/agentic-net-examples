using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_video.pdf";
        const string videoUrl = "https://example.com/video.mp4";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // The ScreenAnnotation constructor requires the media file (or URL) as the third argument.
            // No need to set MediaClipData or PlayOnPageOpen separately – the constructor handles the media reference.
            ScreenAnnotation screen = new ScreenAnnotation(page, rect, videoUrl)
            {
                Name = "VideoAnnotation",
                Title = "Demo Video",
                Contents = "External video playback"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(screen);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with video annotation to '{outputPath}'.");
    }
}
