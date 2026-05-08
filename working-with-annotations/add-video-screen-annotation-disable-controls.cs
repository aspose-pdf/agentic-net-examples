using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string videoFile = "sample.mp4";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(videoFile))
        {
            Console.Error.WriteLine($"Video file not found: {videoFile}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear
            // (left, bottom, right, top) – fully qualified to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create a ScreenAnnotation that references the video file
            ScreenAnnotation screen = new ScreenAnnotation(page, rect, videoFile);

            // Disable user interaction controls by setting annotation flags
            // NoZoom, NoRotate and NoView hide UI elements and prevent user actions
            screen.Flags = AnnotationFlags.NoZoom | AnnotationFlags.NoRotate | AnnotationFlags.NoView;

            // The video will start playing when the annotation is activated.
            // The Action property is read‑only; the constructor already links the media file,
            // so no explicit Action assignment is required.

            // Add the annotation to the page
            page.Annotations.Add(screen);

            // Save the modified PDF (lifecycle rule: use Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Screen annotation added and saved to '{outputPdf}'.");
    }
}
