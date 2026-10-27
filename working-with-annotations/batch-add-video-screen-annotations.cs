using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Video files – one per page (ensure the count matches the number of pages)
        string[] videoFiles = new[]
        {
            "video_page1.mp4",
            "video_page2.mp4",
            "video_page3.mp4"
            // add more paths as needed
        };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Verify that each video file exists before processing
        for (int i = 0; i < videoFiles.Length; i++)
        {
            if (!File.Exists(videoFiles[i]))
            {
                Console.Error.WriteLine($"Video file not found: {videoFiles[i]}");
                return;
            }
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure we do not exceed the number of pages
            int pageCount = doc.Pages.Count;
            int videosToAdd = Math.Min(pageCount, videoFiles.Length);

            // Iterate using 1‑based page indexing (Aspose.Pdf requirement)
            for (int pageIndex = 1; pageIndex <= videosToAdd; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Define the rectangle where the screen annotation will appear
                // Fully qualified type to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create the screen annotation for the current page, passing the video file path
                ScreenAnnotation screen = new ScreenAnnotation(page, rect, videoFiles[pageIndex - 1]);

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(screen);
            }

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Screen annotations added. Output saved to '{outputPdfPath}'.");
    }
}
