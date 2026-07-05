using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "output.pdf";     // result PDF
        const string videoPath = "sample.mp4";     // video to embed

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (coordinates are in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Set the content type to Video
                Type = RichMediaAnnotation.ContentType.Video,

                // Activate on click (Aspose.Pdf does not provide a direct double‑click event;
                // Click is the closest activation event available)
                ActivateOn = RichMediaAnnotation.ActivationEvent.Click
            };

            // Embed the video file into the annotation
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                // The first parameter is the name of the embedded stream
                richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
            }

            // Optionally set a poster image (preview) – omitted here

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rich media annotation added and saved to '{outputPdf}'.");
    }
}