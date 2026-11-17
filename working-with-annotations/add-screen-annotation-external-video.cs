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

        // Load the PDF; the using block guarantees deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            var rect = new Aspose.Pdf.Rectangle(100, 200, 400, 500);

            // ScreenAnnotation takes the media (URL or file path) in its constructor.
            // Automatic playback for external media is the default behavior.
            var screen = new ScreenAnnotation(page, rect, videoUrl)
            {
                // Optional: ensure the annotation is printable/visible if needed.
                Flags = AnnotationFlags.Print
            };

            // Attach the annotation to the page
            page.Annotations.Add(screen);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with video ScreenAnnotation to '{outputPath}'.");
    }
}
