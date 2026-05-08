using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_screen_annotation.pdf";
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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify the document has at least three pages (1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Retrieve page three
            Page page = doc.Pages[3];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a ScreenAnnotation that references the video file
            ScreenAnnotation screen = new ScreenAnnotation(page, rect, videoPath)
            {
                Title = "Sample Video",
                Contents = "Click to play the video"
            };

            // Add the annotation to the page
            page.Annotations.Add(screen);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with screen annotation: {outputPdf}");
    }
}