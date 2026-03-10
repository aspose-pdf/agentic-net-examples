using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string outputHtmlPath = "output.html";
        const string mediaFilePath  = "sample_video.mp4"; // MP4, AVI, etc.

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(mediaFilePath))
        {
            Console.Error.WriteLine($"Multimedia file not found: {mediaFilePath}");
            return;
        }

        try
        {
            // Load the source PDF – wrapped in using for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Choose the page where the annotation will be placed (first page in this example)
                Page page = pdfDoc.Pages[1];

                // Define the annotation rectangle (coordinates are in points; lower‑left origin)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

                // Create a RichMediaAnnotation on the selected page
                RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
                {
                    // Specify that the embedded content is a video – use the enum, not a string
                    Type = RichMediaAnnotation.ContentType.Video
                };

                // Embed the multimedia file into the annotation
                using (FileStream mediaStream = File.OpenRead(mediaFilePath))
                {
                    // The first argument is an arbitrary name for the content stream
                    richMedia.SetContent("VideoContent", mediaStream);
                }

                // Optionally, set a poster image (preview) – omitted here for brevity
                // Example:
                // using (FileStream posterStream = File.OpenRead("poster.jpg"))
                // {
                //     richMedia.SetPoster(posterStream);
                // }

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(richMedia);

                // Prepare HTML save options – required to actually produce HTML output
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Embed all resources (fonts, images, etc.) into the single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Ensure raster images are saved as PNGs embedded into SVG (cross‑platform safe)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the PDF as HTML. The HtmlSaveOptions forces HTML output on all platforms.
                // Wrap in try‑catch to handle the Windows‑only GDI+ limitation gracefully.
                try
                {
                    pdfDoc.Save(outputHtmlPath, htmlOptions);
                    Console.WriteLine($"HTML file with multimedia annotation saved to '{outputHtmlPath}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows GDI+. Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
