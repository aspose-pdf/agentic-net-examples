using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF containing multiple pages
        const string inputPdf = "input.pdf";
        // Output PDF with screen annotations added
        const string outputPdf = "output_with_videos.pdf";

        // Video files to be attached – one per page (order matters)
        // Ensure the array length matches the number of pages in the PDF
        string[] videoFiles = new string[]
        {
            "video1.mp4",
            "video2.mp4",
            "video3.mp4"
            // Add more entries if the PDF has more pages
        };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Verify that all video files exist before processing
        foreach (var vf in videoFiles)
        {
            if (!File.Exists(vf))
            {
                Console.Error.WriteLine($"Video file not found: {vf}");
                return;
            }
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count; // 1‑based indexing

            // Iterate over each page and add a ScreenAnnotation
            for (int i = 1; i <= pageCount; i++)
            {
                // If there are fewer video files than pages, stop adding annotations
                if (i > videoFiles.Length)
                    break;

                Page page = doc.Pages[i];
                string videoPath = videoFiles[i - 1];

                // Define the annotation rectangle (example: 200x150 rectangle at lower‑left corner)
                // Rectangle constructor: (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 50, 250, 200);

                // Create the screen annotation with the page, rectangle, and video file path
                ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, videoPath)
                {
                    // Optional: set a title or contents for the annotation
                    Title = $"Video for page {i}",
                    Contents = $"Play video: {Path.GetFileName(videoPath)}"
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(screenAnn);
            }

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Screen annotations added. Output saved to '{outputPdf}'.");
    }
}