using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_video.pdf";
        const string videoPath = "sample.mp4";

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

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Desired width for the video annotation (in points)
            const double annotationWidth = 300.0;
            // Assume a 16:9 aspect ratio for the video
            const double aspectRatio = 16.0 / 9.0;
            double annotationHeight = annotationWidth / aspectRatio;

            // Add a ScreenAnnotation to each page
            foreach (Page page in doc.Pages)
            {
                // Position the annotation at the top‑left corner of the page
                double llx = 50; // lower‑left X
                double lly = page.PageInfo.Height - 50 - annotationHeight; // lower‑left Y
                double urx = llx + annotationWidth; // upper‑right X
                double ury = lly + annotationHeight; // upper‑right Y

                // Fully qualified Rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create the screen annotation first
                ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, videoPath);
                // Optional visual styling
                screenAnn.Color = Aspose.Pdf.Color.LightGray;
                // Border must be set after the annotation instance exists
                screenAnn.Border = new Border(screenAnn) { Width = 1 };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(screenAnn);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Video annotations added. Output saved to '{outputPdf}'.");
    }
}
