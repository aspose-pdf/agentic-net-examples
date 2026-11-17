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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate using 1‑based page indexing (Aspose.Pdf uses 1‑based indexes)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get page dimensions (points)
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Desired width: 80% of page width
                double rectWidth = pageWidth * 0.8;

                // Video aspect ratio (e.g., 16:9)
                const double videoAspect = 16.0 / 9.0;

                // Compute height to preserve aspect ratio
                double rectHeight = rectWidth / videoAspect;

                // Center the rectangle on the page
                double llx = (pageWidth  - rectWidth) / 2; // lower‑left X
                double lly = (pageHeight - rectHeight) / 2; // lower‑left Y
                double urx = llx + rectWidth; // upper‑right X
                double ury = lly + rectHeight; // upper‑right Y

                // Fully qualified rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create a ScreenAnnotation for the current page – media file is passed via constructor
                ScreenAnnotation screenAnno = new ScreenAnnotation(page, rect, videoFile)
                {
                    // Optional title shown in the annotation UI
                    Title = $"Page {i} Video"
                    // MediaType property does not exist in current Aspose.Pdf versions and is therefore omitted.
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(screenAnno);
            }

            // Save the modified PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Video annotations added. Output saved to '{outputPdf}'.");
    }
}
