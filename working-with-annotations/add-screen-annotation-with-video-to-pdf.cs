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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
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
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Get page three (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[3];

            // Define the rectangle where the screen annotation will appear
            // (left, bottom, width, height) – adjust values as needed
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 200);

            // Create the ScreenAnnotation with the video file
            ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, videoFile);

            // Optionally set a title or contents
            screenAnn.Title    = "Sample Video";
            screenAnn.Contents = "Click to play the video.";

            // Add the annotation to the page
            page.Annotations.Add(screenAnn);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Screen annotation added and saved to '{outputPdf}'.");
    }
}