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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Assume a 16:9 video aspect ratio (height / width)
            const double videoAspect = 9.0 / 16.0;

            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Choose a desired width for the annotation (e.g., 200 points or half the page width)
                double desiredWidth = Math.Min(200, page.PageInfo.Width / 2);
                double desiredHeight = desiredWidth * videoAspect; // maintain aspect ratio

                // Define rectangle position with a margin from the lower‑left corner
                double llx = 50;                     // lower‑left X
                double lly = 50;                     // lower‑left Y
                double urx = llx + desiredWidth;     // upper‑right X
                double ury = lly + desiredHeight;    // upper‑right Y

                // Fully qualified rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create the screen annotation that will play the video
                ScreenAnnotation screen = new ScreenAnnotation(page, rect, videoPath)
                {
                    Title    = "Video",
                    Contents = "Click to play the video"
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(screen);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with video annotations saved to '{outputPath}'.");
    }
}