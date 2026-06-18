using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_screen_annotations.pdf";

        // Video files to be attached – one per page (repeat if fewer than pages)
        string[] videoFiles = { "video1.mp4", "video2.mp4", "video3.mp4" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Optional: verify that video files exist
        foreach (var vf in videoFiles)
        {
            if (!File.Exists(vf))
            {
                Console.Error.WriteLine($"Video file not found: {vf}");
                return;
            }
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count;

            // Iterate pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= pageCount; i++)
            {
                // Select a video file for the current page (wrap around if needed)
                string mediaPath = videoFiles[(i - 1) % videoFiles.Length];

                // Define the annotation rectangle (example coordinates)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 300);

                // Create a ScreenAnnotation using the constructor (ScreenAnnotation(Page, Rectangle, string))
                ScreenAnnotation screenAnn = new ScreenAnnotation(doc.Pages[i], rect, mediaPath)
                {
                    Title = $"Video {i}",
                    Contents = $"Play video {Path.GetFileName(mediaPath)}"
                };

                // Add the annotation to the page's annotation collection
                doc.Pages[i].Annotations.Add(screenAnn);
            }

            // Save the modified PDF (save-to-non-pdf rule: Save writes PDF regardless of extension)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Saved PDF with screen annotations to '{outputPdf}'.");
    }
}