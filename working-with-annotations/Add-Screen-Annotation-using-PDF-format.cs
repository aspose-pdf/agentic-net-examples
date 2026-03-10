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
        const string mediaPath = "sample.mp4"; // path to the multimedia file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(mediaPath))
        {
            Console.Error.WriteLine($"Media file not found: {mediaPath}");
            return;
        }

        // Load the PDF document (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Select the page where the annotation will be placed
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a ScreenAnnotation that references the multimedia file
            ScreenAnnotation screen = new ScreenAnnotation(page, rect, mediaPath);

            // Optional: set title and contents for the annotation
            screen.Title = "Video Annotation";
            screen.Contents = "Click to play the video";

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(screen);

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Screen annotation added and saved to '{outputPath}'.");
    }
}