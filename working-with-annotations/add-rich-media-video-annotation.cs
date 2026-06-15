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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // Load the PDF (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Set the content type to Video
                Type = RichMediaAnnotation.ContentType.Video,

                // Activate on click (the closest to double‑click; PDF spec does not define double‑click)
                ActivateOn = RichMediaAnnotation.ActivationEvent.Click
            };

            // Embed the video stream
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                // The first argument is the name of the embedded file
                richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
            }

            // Optionally set a poster image (first frame) – omitted here

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rich media annotation added and saved to '{outputPdf}'.");
    }
}