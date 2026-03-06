using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "multimedia.pdf";
        const string videoPath = "sample.mp4";
        const string svgPath = "multimedia.svg";

        // Verify that the video file exists.
        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // Create a new PDF document and add a RichMediaAnnotation that embeds the video.
        using (Document doc = new Document())
        {
            // Add a blank page to the document.
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (left, bottom, right, top).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Initialize the RichMediaAnnotation.
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Specify that the embedded content is a video.
                Type = RichMediaAnnotation.ContentType.Video,
                // Optional visual appearance settings.
                Color = Aspose.Pdf.Color.LightGray,
                Contents = "Sample video"
            };

            // Embed the video file into the annotation.
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                // The first argument is a name for the embedded content.
                richMedia.SetContent("video.mp4", videoStream);
            }

            // Add the annotation to the page.
            page.Annotations.Add(richMedia);

            // Save the document as SVG using SvgSaveOptions.
            SvgSaveOptions svgOptions = new SvgSaveOptions();
            doc.Save(svgPath, svgOptions);
        }

        Console.WriteLine($"PDF with multimedia saved as SVG: {svgPath}");
    }
}