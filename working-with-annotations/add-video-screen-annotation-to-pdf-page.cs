using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and video file paths
        const string inputPdf = "input.pdf";
        const string videoFile = "sample.mp4";
        const string outputPdf = "output_with_screen_annotation.pdf";

        // Verify files exist
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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least three pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Get page three
            Page page = doc.Pages[3];

            // Define the rectangle where the screen annotation will appear
            // Coordinates are in points (1/72 inch). Adjust as needed.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the ScreenAnnotation with the video file
            ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, videoFile);

            // Optionally set a title or contents
            screenAnn.Title = "Sample Video";
            screenAnn.Contents = "Click to play the video.";

            // Add the annotation to the page
            page.Annotations.Add(screenAnn);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with screen annotation: {outputPdf}");
    }
}