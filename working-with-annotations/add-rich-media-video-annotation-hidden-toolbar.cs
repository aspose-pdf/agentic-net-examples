using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf"; // result PDF
        const string videoPath = "sample.mp4"; // video to embed

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the RichMedia annotation will appear
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Activate on click
                ActivateOn = RichMediaAnnotation.ActivationEvent.Click,
                // Specify that the content is a video (use the enum, not a string)
                Type = RichMediaAnnotation.ContentType.Video
            };

            // Set the video content stream
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
            }

            // Hide the default playback toolbar by providing custom flash variables.
            // The property expects a string, so assign the string directly.
            string flashVars = "toolbar=false;ui=false;";
            richMedia.CustomFlashVariables = flashVars;

            // Optionally set a poster image (first frame) – omitted for brevity

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"RichMedia annotation added and saved to '{outputPdf}'.");
    }
}
