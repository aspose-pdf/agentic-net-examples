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
        const string videoPath = "sample_video.mp4";

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

        // Assume a known video aspect ratio (width:height). For example, 16:9.
        const double videoAspectRatio = 16.0 / 9.0; // width / height

        // Desired width of the annotation as a fraction of the page width.
        const double widthFraction = 0.5; // 50% of page width

        using (Document doc = new Document(inputPdf))
        {
            foreach (Page page in doc.Pages)
            {
                // Page dimensions
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Compute annotation width and height while preserving aspect ratio
                double annotWidth  = pageWidth * widthFraction;
                double annotHeight = annotWidth / videoAspectRatio;

                // Ensure the annotation fits vertically; if not, scale down proportionally
                if (annotHeight > pageHeight * widthFraction)
                {
                    annotHeight = pageHeight * widthFraction;
                    annotWidth  = annotHeight * videoAspectRatio;
                }

                // Center the annotation on the page
                double llx = (pageWidth  - annotWidth)  / 2.0;
                double lly = (pageHeight - annotHeight) / 2.0;
                double urx = llx + annotWidth;
                double ury = lly + annotHeight;

                // Create the rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create the screen annotation with the video file
                ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, videoPath);

                // Optionally set a title or contents
                screenAnn.Title    = "Embedded Video";
                screenAnn.Contents = "Click to play the video";

                // Add the annotation to the page
                page.Annotations.Add(screenAnn);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Video annotations added. Output saved to '{outputPdf}'.");
    }
}