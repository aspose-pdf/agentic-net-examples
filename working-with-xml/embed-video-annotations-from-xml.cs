using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_with_videos.pdf";
        const string videoXmlPath   = "videos.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(videoXmlPath))
        {
            Console.Error.WriteLine($"Video XML not found: {videoXmlPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Parse the XML that contains video references
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(videoXmlPath);

                // Expected XML format:
                // <Videos>
                //   <Video page="1" x="100" y="500" width="200" height="150" src="sample.mp4" />
                //   ...
                // </Videos>
                XmlNodeList videoNodes = xmlDoc.SelectNodes("//Video");
                if (videoNodes != null)
                {
                    foreach (XmlNode node in videoNodes)
                    {
                        // Extract attributes with defaults
                        int pageNumber = int.Parse(node.Attributes["page"]?.Value ?? "1");
                        double x        = double.Parse(node.Attributes["x"]?.Value ?? "0");
                        double y        = double.Parse(node.Attributes["y"]?.Value ?? "0");
                        double width    = double.Parse(node.Attributes["width"]?.Value ?? "200");
                        double height   = double.Parse(node.Attributes["height"]?.Value ?? "150");
                        string srcPath  = node.Attributes["src"]?.Value;

                        if (string.IsNullOrEmpty(srcPath) || !File.Exists(srcPath))
                        {
                            Console.Error.WriteLine($"Video file not found: {srcPath}");
                            continue;
                        }

                        // Ensure the requested page exists
                        if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                        {
                            Console.Error.WriteLine($"Invalid page number {pageNumber} for video {srcPath}");
                            continue;
                        }

                        Page targetPage = pdfDoc.Pages[pageNumber];

                        // Define the rectangle where the annotation will appear
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(x, y, x + width, y + height);

                        // Create the RichMediaAnnotation
                        RichMediaAnnotation richMedia = new RichMediaAnnotation(targetPage, rect)
                        {
                            // Set the type to Video
                            Type = RichMediaAnnotation.ContentType.Video,
                            // Optional: give a tooltip or description
                            Contents = $"Video: {Path.GetFileName(srcPath)}"
                        };

                        // Embed the video content
                        using (FileStream videoStream = File.OpenRead(srcPath))
                        {
                            // The first argument is a name for the embedded stream; it can be the file name.
                            richMedia.SetContent(Path.GetFileName(srcPath), videoStream);
                        }

                        // Add the annotation to the page
                        targetPage.Annotations.Add(richMedia);
                    }
                }

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"PDF with embedded videos saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}