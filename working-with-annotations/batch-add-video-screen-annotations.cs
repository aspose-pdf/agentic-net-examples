using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_video.pdf";
        const string videoPath  = "sample.mp4";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // Placeholder video dimensions – replace with actual values if known
        const double videoWidthPx  = 640;
        const double videoHeightPx = 360;
        double aspect = videoWidthPx / videoHeightPx; // width / height

        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                // Desired annotation width: half of page width
                double rectWidth  = page.PageInfo.Width / 2;
                double rectHeight = rectWidth / aspect; // maintain aspect ratio

                // Position rectangle with a margin from top‑left corner
                double llx = 50;                                 // left
                double lly = page.PageInfo.Height - rectHeight - 50; // bottom
                double urx = llx + rectWidth;                    // right
                double ury = lly + rectHeight;                   // top

                // Fully qualified rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create screen annotation that plays the video
                ScreenAnnotation screen = new ScreenAnnotation(page, rect, videoPath);
                screen.Title    = "Video Annotation";
                screen.Contents = "Click to play video";

                // Add the annotation to the page
                page.Annotations.Add(screen);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with video annotations to '{outputPath}'.");
    }
}