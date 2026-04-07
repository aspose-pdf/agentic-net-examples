using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_screen.pdf";
        const string videoUrl = "https://example.com/video.mp4";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a ScreenAnnotation; a dummy media file path is required by the ctor
            ScreenAnnotation screen = new ScreenAnnotation(page, rect, "");

            // Optional visual settings
            screen.Title = "Video Annotation";
            screen.Color = Aspose.Pdf.Color.Yellow;

            // Add an action that opens the external video URL
            screen.Actions.Add(new GoToURIAction(videoUrl));

            // Attach the annotation to the page
            page.Annotations.Add(screen);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ScreenAnnotation to '{outputPath}'.");
    }
}