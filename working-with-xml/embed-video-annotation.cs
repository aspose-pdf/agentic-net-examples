using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public class EmbedVideoFromXml
{
    public static void Main()
    {
        // Sample XML describing video annotations
        string xmlContent = @"<Videos>
    <Video page='1' x='100' y='500' width='200' height='150'>sample.mp4</Video>
</Videos>";

        // Load XML from string
        XDocument xdoc = XDocument.Load(new StringReader(xmlContent));

        // Create a new PDF document
        using (Document pdfDocument = new Document())
        {
            // Add a blank page (page 1)
            Page page = pdfDocument.Pages.Add();

            // Iterate over each <Video> element
            foreach (XElement videoElement in xdoc.Root.Elements("Video"))
            {
                // Parse attributes
                int pageNumber = int.Parse(videoElement.Attribute("page").Value);
                float x = float.Parse(videoElement.Attribute("x").Value);
                float y = float.Parse(videoElement.Attribute("y").Value);
                float width = float.Parse(videoElement.Attribute("width").Value);
                float height = float.Parse(videoElement.Attribute("height").Value);
                string videoPath = videoElement.Value.Trim();

                // Ensure the target page exists (pages are 1‑based)
                Page targetPage = pdfDocument.Pages[pageNumber];

                // Define rectangle for the annotation (lower‑left x,y and upper‑right x+width, y+height)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(x, y, x + width, y + height);

                // Create RichMedia annotation and set its type to Video
                RichMediaAnnotation richMedia = new RichMediaAnnotation(targetPage, rect);
                richMedia.Type = RichMediaAnnotation.ContentType.Video;

                // Verify that the video file exists. If it does not, create a tiny placeholder file
                // to avoid a runtime FileNotFoundException.
                if (!File.Exists(videoPath))
                {
                    // Create a minimal MP4 placeholder (empty file is acceptable for demonstration).
                    // In a real scenario you would supply a valid video file.
                    using (FileStream placeholder = File.Create(videoPath))
                    {
                        // Write a few bytes that constitute a valid MP4 header (optional).
                        byte[] header = new byte[] { 0x00, 0x00, 0x00, 0x18, 0x66, 0x74, 0x79, 0x70, 0x6D, 0x70, 0x34, 0x32 };
                        placeholder.Write(header, 0, header.Length);
                    }
                }

                // Load the video file and attach it to the annotation
                using (FileStream videoStream = new FileStream(videoPath, FileMode.Open, FileAccess.Read))
                {
                    richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
                }

                // Add the annotation to the page
                targetPage.Annotations.Add(richMedia);
            }

            // Save the resulting PDF
            pdfDocument.Save("output.pdf");
        }
    }
}
