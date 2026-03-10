using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "output_multimedia.pdf"; // result PDF
        const string videoPath  = "sample.mp4";         // video to embed

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

        // Load the PDF, add a RichMediaAnnotation, and save.
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page).
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle for the annotation (position and size).
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation.
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);
            richMedia.Contents = "Embedded video example";
            // The Type property expects a ContentType enum, not a string.
            richMedia.Type = RichMediaAnnotation.ContentType.Video;
            // Set a simple border – the Border constructor needs the annotation instance.
            richMedia.Border = new Border(richMedia) { Width = 1 };
            // If you want a colored border and your Aspose version supports it, uncomment the line below:
            // richMedia.Border.Color = Aspose.Pdf.Color.Blue;

            // Attach the video file as the content stream.
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                // The first parameter is a name for the content (can be any string).
                richMedia.SetContent("VideoContent", videoStream);
            }

            // Optionally set a poster image (preview) – omitted here.

            // Add the annotation to the page.
            page.Annotations.Add(richMedia);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Multimedia PDF saved to '{outputPdf}'.");
    }
}
