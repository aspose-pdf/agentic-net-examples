using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_videos.pdf";

        // Video files – one file per page (order matters)
        string[] videoFiles = {
            "video1.mp4",
            "video2.mp4",
            "video3.mp4"
            // add more paths if the PDF has more pages
        };

        // Validate input PDF
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Validate video files
        foreach (var v in videoFiles)
        {
            if (!File.Exists(v))
            {
                Console.Error.WriteLine($"Video file not found: {v}");
                return;
            }
        }

        // Open PDF inside a using block (deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count;

            // Ensure we have at least as many videos as pages
            if (videoFiles.Length < pageCount)
            {
                Console.Error.WriteLine($"Insufficient video files ({videoFiles.Length}) for {pageCount} pages.");
                return;
            }

            // Pages are 1‑based – iterate accordingly
            for (int i = 1; i <= pageCount; i++)
            {
                Page page = doc.Pages[i];

                // Define the annotation rectangle (example: 200x200 box at (100,100))
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 300);

                // Create a ScreenAnnotation on the current page, passing the video file path as the required third argument
                ScreenAnnotation screen = new ScreenAnnotation(page, rect, videoFiles[i - 1]);

                // Optional: set title and contents for accessibility
                screen.Title    = $"Video for page {i}";
                screen.Contents = $"Embedded video: {Path.GetFileName(videoFiles[i - 1])}";

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(screen);
            }

            // Save the modified PDF (PDF format is default)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with video annotations saved to '{outputPdf}'.");
    }
}
