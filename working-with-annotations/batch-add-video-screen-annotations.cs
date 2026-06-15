using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_video.pdf";
        const string videoFile = "sample_video.mp4";

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Page dimensions
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Desired annotation width (e.g., 50% of page width)
                double annWidth = pageWidth * 0.5;

                // Assume a 16:9 video aspect ratio
                double aspectRatio = 9.0 / 16.0;
                double annHeight = annWidth * aspectRatio;

                // Center the annotation on the page
                double llx = (pageWidth  - annWidth)  / 2.0;
                double lly = (pageHeight - annHeight) / 2.0;
                double urx = llx + annWidth;
                double ury = lly + annHeight;

                // Fully qualified rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create the screen annotation with the video file
                ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, videoFile);

                // Add the annotation to the page
                page.Annotations.Add(screenAnn);
            }

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with video annotations saved to '{outputPdf}'.");
    }
}