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
        const string videoPath = "sample.mp4";

        if (!File.Exists(inputPdf) || !File.Exists(videoPath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPdf))
        {
            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create a RichMediaAnnotation on the first page
            RichMediaAnnotation richMedia = new RichMediaAnnotation(doc.Pages[1], rect);

            // Activate the media on click
            richMedia.ActivateOn = RichMediaAnnotation.ActivationEvent.Click;

            // Hide the default playback toolbar (flash variable example)
            richMedia.CustomFlashVariables = "toolbar=0";

            // Embed the video content
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
            }

            // Optional: set a poster image for the annotation
            // using (FileStream posterStream = File.OpenRead("poster.jpg"))
            // {
            //     richMedia.SetPoster(posterStream);
            // }

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rich media annotation added and saved to '{outputPdf}'.");
    }
}