using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and output PDF paths
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_with_screen_annotations.pdf";

        // Video files – one per page (ensure the array length matches the number of pages)
        string[] videoFiles = new string[]
        {
            "video1.mp4",
            "video2.mp4",
            "video3.mp4"
            // Add more entries as needed for additional pages
        };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Verify that we have enough video files for the pages
            if (videoFiles.Length < doc.Pages.Count)
            {
                Console.Error.WriteLine("Not enough video files provided for the number of pages.");
                return;
            }

            // Iterate over pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define the rectangle where the screen annotation will appear
                // (left, bottom, width, height) – adjust values as needed
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 200);

                // Create the ScreenAnnotation with the corresponding video file
                ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, videoFiles[i - 1])
                {
                    // Optional: set a title or contents for the annotation
                    Title    = $"Video {i}",
                    Contents = $"Play video {Path.GetFileName(videoFiles[i - 1])}"
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(screenAnn);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with screen annotations: {outputPdfPath}");
    }
}