using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to input XSL‑FO file, video file and output PDF
        const string xslFoPath = "input.xslfo";
        const string videoPath = "sample.mp4";
        const string outputPdf = "output.pdf";

        // Validate input files
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }
        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // Load XSL‑FO into a PDF document
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();
        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // Ensure at least one page exists
            Page page = pdfDoc.Pages[1];

            // Define the rectangle for the multimedia annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);
            // The ContentType property does not exist on RichMediaAnnotation; SetContent infers the type.
            richMedia.Color = Aspose.Pdf.Color.Transparent; // No background color
            richMedia.Border = new Border(richMedia) // Border requires the parent annotation
            {
                Width = 1,
                Style = BorderStyle.Solid
            };

            // Attach video content (MP4 in this example)
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                richMedia.SetContent("video/mp4", videoStream);
            }

            // Optional: set a poster (thumbnail) image for the annotation
            // using (FileStream posterStream = File.OpenRead("poster.jpg"))
            // {
            //     richMedia.SetPoster(posterStream);
            // }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with multimedia annotation saved to '{outputPdf}'.");
    }
}
