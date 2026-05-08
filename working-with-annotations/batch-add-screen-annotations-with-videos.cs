using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_screen_annotations.pdf";

        // Video files to embed – one per page (wrap around if fewer files than pages)
        string[] videoFiles = { "video1.mp4", "video2.mp4", "video3.mp4" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count; // 1‑based indexing

            for (int i = 1; i <= pageCount; i++)
            {
                // Select a video file for the current page
                string videoPath = videoFiles[(i - 1) % videoFiles.Length];

                // Define the annotation rectangle (left, bottom, right, top)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

                // Create the ScreenAnnotation on the current page
                ScreenAnnotation screen = new ScreenAnnotation(doc.Pages[i], rect, videoPath)
                {
                    Title    = $"Video {i}",
                    Contents = $"Play {Path.GetFileName(videoPath)}"
                };

                // Add the annotation to the page's annotation collection
                doc.Pages[i].Annotations.Add(screen);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with screen annotations saved to '{outputPdf}'.");
    }
}