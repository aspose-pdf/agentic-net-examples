using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string videoPath = "sample_video.mp4";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the video player will appear
            var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create the screen annotation – the media file is supplied via the constructor
            var screen = new ScreenAnnotation(page, rect, videoPath);
            screen.Contents = "Embedded video"; // optional description

            // Hide the annotation border (optional)
            screen.Border = new Border(screen) { Width = 0 };

            // Ensure the annotation is printable/visible (optional flag configuration)
            screen.Flags = AnnotationFlags.Print;

            // Add the annotation to the page
            page.Annotations.Add(screen);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with video annotation saved to '{outputPdfPath}'.");
    }
}
