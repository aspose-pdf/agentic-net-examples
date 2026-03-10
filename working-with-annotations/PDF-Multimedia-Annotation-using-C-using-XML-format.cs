using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths for the output PDF and the XFDF (XML) file
        const string outputPdfPath = "multimedia_annotation.pdf";
        const string xfdfPath      = "multimedia_annotation.xfdf";
        // Path to the multimedia file (e.g., a video file)
        const string mediaFilePath = "sample_video.mp4";

        // Ensure the media file exists before proceeding
        if (!File.Exists(mediaFilePath))
        {
            Console.Error.WriteLine($"Media file not found: {mediaFilePath}");
            return;
        }

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a RichMediaAnnotation (video/audio) on the page
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Optional visual properties
                Color = Aspose.Pdf.Color.LightGray,
                // Set the type of content (Video or Audio)
                Type = RichMediaAnnotation.ContentType.Video,
                // Provide a title/description
                Contents = "Sample video annotation"
            };

            // Attach the multimedia content to the annotation
            // The SetContent method accepts a MIME type and a stream with the media data
            using (FileStream mediaStream = File.OpenRead(mediaFilePath))
            {
                // MIME type for MP4 video
                richMedia.SetContent("video/mp4", mediaStream);
            }

            // Optionally set a poster image (preview) – omitted here for brevity

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(richMedia);

            // Save the PDF document (default PDF format)
            doc.Save(outputPdfPath);

            // Export all annotations (including the multimedia one) to XFDF (XML) format
            // This uses the built‑in ExportAnnotationsToXfdf method
            doc.ExportAnnotationsToXfdf(xfdfPath);
        }

        Console.WriteLine($"PDF with multimedia annotation saved to '{outputPdfPath}'.");
        Console.WriteLine($"Annotations exported to XFDF (XML) at '{xfdfPath}'.");
    }
}