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
        const string videoFile = "sample.mp4";     // video to embed

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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page, 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle for the annotation (left, bottom, width, height)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 300);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Set the type of the embedded content to Video
                Type = RichMediaAnnotation.ContentType.Video,

                // Activate only on click (Aspose.Pdf does not provide a double‑click event;
                // Click is the closest available activation event)
                ActivateOn = RichMediaAnnotation.ActivationEvent.Click
            };

            // Embed the video stream into the annotation
            using (FileStream videoStream = File.OpenRead(videoFile))
            {
                // The first parameter is the name of the stream inside the PDF
                richMedia.SetContent(Path.GetFileName(videoFile), videoStream);
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rich media annotation added and saved to '{outputPdf}'.");
    }
}