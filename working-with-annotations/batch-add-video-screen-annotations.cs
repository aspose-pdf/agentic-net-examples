using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_with_video.pdf";
        const string videoFilePath  = "sample.mp4";

        // Define the video aspect ratio (width : height). Example: 16:9
        const double videoWidthRatio  = 16.0;
        const double videoHeightRatio = 9.0;

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(videoFilePath))
        {
            Console.Error.WriteLine($"Video file not found: {videoFilePath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate using 1‑based page indexing (Aspose.Pdf rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Page dimensions (points)
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Desired rectangle size: 80% of page width, maintain aspect ratio
                double rectWidth  = pageWidth * 0.8;
                double rectHeight = rectWidth * (videoHeightRatio / videoWidthRatio);

                // If height exceeds 80% of page height, scale down based on height instead
                if (rectHeight > pageHeight * 0.8)
                {
                    rectHeight = pageHeight * 0.8;
                    rectWidth  = rectHeight * (videoWidthRatio / videoHeightRatio);
                }

                // Center the rectangle on the page
                double llx = (pageWidth  - rectWidth)  / 2.0; // lower‑left X
                double lly = (pageHeight - rectHeight) / 2.0; // lower‑left Y
                double urx = llx + rectWidth;                // upper‑right X
                double ury = lly + rectHeight;               // upper‑right Y

                // Fully qualified Rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create the ScreenAnnotation and attach the video file via the constructor that accepts the media file path
                ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, videoFilePath)
                {
                    // Optional: set a tooltip or description
                    Contents = "Embedded video"
                };

                // Add the annotation to the page
                page.Annotations.Add(screenAnn);
            }

            // Save the modified PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Video annotations added. Output saved to '{outputPdfPath}'.");
    }
}
