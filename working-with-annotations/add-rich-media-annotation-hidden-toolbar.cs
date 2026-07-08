using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // result PDF
        const string videoFile = "sample.mp4";         // video to embed

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
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle for the RichMediaAnnotation (coordinates in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Activate the media on click (you can also use PageOpen or PageVisible)
                ActivateOn = RichMediaAnnotation.ActivationEvent.Click,

                // Hide the default playback toolbar by supplying custom flash variables.
                // The exact variable name depends on the player; "toolbar=false" is a common example.
                CustomFlashVariables = "toolbar=false"
            };

            // Embed the video content. The first argument is a name for the stream.
            using (FileStream videoStream = File.OpenRead(videoFile))
            {
                richMedia.SetContent(Path.GetFileName(videoFile), videoStream);
            }

            // Optionally set a poster image (preview shown before playback)
            // using (FileStream posterStream = File.OpenRead("poster.jpg"))
            // {
            //     richMedia.SetPoster(posterStream);
            // }

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(richMedia);

            // Save the modified PDF (lifecycle rule: use using, then Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"RichMediaAnnotation added and saved to '{outputPdf}'.");
    }
}