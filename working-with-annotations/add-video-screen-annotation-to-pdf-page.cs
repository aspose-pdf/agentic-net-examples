using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and video file paths
        const string inputPdfPath  = "input.pdf";
        const string videoFilePath = "sample_video.mp4";
        const string outputPdfPath = "output_with_screen_annotation.pdf";

        // Verify that the input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(videoFilePath))
        {
            Console.Error.WriteLine($"Video file not found: {videoFilePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
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
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create the ScreenAnnotation with the video file
            ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, videoFilePath);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(screenAnn);

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with screen annotation: {outputPdfPath}");
    }
}