using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_screen_annotation.pdf";
        const string videoUrl = "https://example.com/video.mp4";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the annotation will appear (llx, lly, urx, ury)
            Rectangle rect = new Rectangle(100, 500, 300, 600);

            // Create a ScreenAnnotation. The third parameter is the media file path/URL.
            // Aspose.PDF accepts an external URL directly in this parameter.
            ScreenAnnotation screen = new ScreenAnnotation(doc.Pages[1], rect, videoUrl)
            {
                Color = Aspose.Pdf.Color.LightGray,
                Title = "Video Annotation",
                Contents = "External video"
                // Auto‑play is the default behavior for external media in recent Aspose.PDF versions.
                // If a specific flag is required, it can be set via the annotation's Flags property.
            };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(screen);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ScreenAnnotation: {outputPath}");
    }
}
