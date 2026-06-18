using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input video file (must be a supported format, e.g., MP4)
        const string videoPath = "sample_video.mp4";
        // Output PDF file
        const string outputPdf = "output_with_screen_annotation.pdf";

        // Verify that the video file exists
        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the video will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 500, 800);

            // Create the ScreenAnnotation with the video file
            ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, videoPath);

            // Add JavaScript to enable looping and hide user controls.
            // The script accesses the annotation's media player and sets loop = true,
            // then disables the default UI controls.
            string js = @"
                var annot = this.getAnnot(this.page, this.name);
                if (annot && annot.media) {
                    annot.media.loop = true;          // Play in a continuous loop
                    annot.media.controls = false;    // Hide playback controls
                }
            ";
            // Add the JavaScript action to the annotation's action list
            screenAnn.Actions.Add(new JavascriptAction(js));

            // Optionally set the annotation to be invisible (no border)
            screenAnn.Border = new Border(screenAnn) { Width = 0 };
            screenAnn.Color = Aspose.Pdf.Color.Transparent;

            // Add the annotation to the page
            page.Annotations.Add(screenAnn);

            // Save the PDF document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with screen annotation saved to '{outputPdf}'.");
    }
}