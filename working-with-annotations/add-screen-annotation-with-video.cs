using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_screen_annotation.pdf";
        const string videoPath  = "sample_video.mp4"; // path to the video file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
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
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Get page three (Aspose.Pdf uses 1‑based indexing)
            Page pageThree = doc.Pages[3];

            // Define the rectangle where the screen annotation will appear
            // (left, bottom, right, top) in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 300, 600);

            // Create the ScreenAnnotation with the video file
            ScreenAnnotation screenAnn = new ScreenAnnotation(pageThree, rect, videoPath)
            {
                // Optional: set a title and contents for the annotation
                Title    = "Video Annotation",
                Contents = "Click to play the video."
            };

            // Add the annotation to the page's annotation collection
            pageThree.Annotations.Add(screenAnn);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with screen annotation: {outputPath}");
    }
}