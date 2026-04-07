using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document (page index will be 1)
            Page page = doc.Pages.Add();

            // Define the rectangle where the RichMedia annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation on page 1
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // Activate the annotation on mouse click
            richMedia.ActivateOn = RichMediaAnnotation.ActivationEvent.Click;

            // Specify that the embedded content is a video (assign enum directly, not a string)
            richMedia.Type = RichMediaAnnotation.ContentType.Video;

            // Set Flash variables (e.g., autoplay and loop control)
            richMedia.CustomFlashVariables = "autoplay=true;loop=false;";

            // -----------------------------------------------------------------
            // Instead of loading external SWF files (which may not exist at runtime),
            // create in‑memory placeholder streams. This prevents FileNotFoundException
            // while still demonstrating the API usage. In a real scenario replace the
            // MemoryStream content with actual SWF bytes (e.g., read from a file or
            // embed as a resource).
            // -----------------------------------------------------------------
            using (MemoryStream playerStream = new MemoryStream())
            using (MemoryStream videoStream = new MemoryStream())
            {
                // Write minimal placeholder data so the streams are not empty.
                // Some PDF viewers expect a non‑empty stream for the player/video.
                byte[] placeholder = new byte[] { 0x46, 0x57, 0x53 }; // "FWS" – typical SWF header start
                playerStream.Write(placeholder, 0, placeholder.Length);
                playerStream.Position = 0;

                videoStream.Write(placeholder, 0, placeholder.Length);
                videoStream.Position = 0;

                // Assign the custom player stream
                richMedia.CustomPlayer = playerStream;

                // Embed the video content stream
                richMedia.SetContent("video.swf", videoStream);

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(richMedia);

                // Save the PDF with the embedded RichMedia annotation
                doc.Save("richmedia_output.pdf");
            }
        }

        Console.WriteLine("RichMedia annotation with Flash video created successfully.");
    }
}
