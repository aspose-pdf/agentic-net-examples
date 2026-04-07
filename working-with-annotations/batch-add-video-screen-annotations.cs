using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and video file paths
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

        // Assume a known video aspect ratio (width:height). For example, 16:9.
        const double videoAspectRatio = 9.0 / 16.0; // height / width

        // Process the document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Page dimensions (points)
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Desired width of the annotation: 50% of page width
                double annotWidth = pageWidth * 0.5;
                // Height calculated to preserve video aspect ratio
                double annotHeight = annotWidth * videoAspectRatio;

                // Center the rectangle on the page
                double llx = (pageWidth  - annotWidth) / 2.0; // lower‑left X
                double lly = (pageHeight - annotHeight) / 2.0; // lower‑left Y
                double urx = llx + annotWidth; // upper‑right X
                double ury = lly + annotHeight; // upper‑right Y

                // Fully qualified Rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create the screen annotation with the video file
                ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, videoFile);

                // Add the annotation to the page
                page.Annotations.Add(screenAnn);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with video annotations saved to '{outputPdf}'.");
    }
}