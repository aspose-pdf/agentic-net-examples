using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string videoPath = "sample.mp4";

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Select the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle area for the screen annotation (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create a ScreenAnnotation that references the video file
            ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, videoPath);

            // Optional: set a title for the annotation
            screenAnn.Title = "Embedded Video";

            // NOTE: To enable loop playback and hide user controls, additional
            // properties or actions may need to be configured (e.g., RichMediaAction,
            // annotation flags, or custom JavaScript). These settings are beyond the
            // basic constructor usage and would depend on the specific PDF viewer
            // capabilities.

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(screenAnn);

            // Save the modified PDF document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Screen annotation with video saved to '{outputPdf}'.");
    }
}