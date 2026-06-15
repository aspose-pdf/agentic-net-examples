using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string inputXmlPath  = "videos.xml";
        const string outputPdfPath = "output_with_videos.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(inputXmlPath))
        {
            Console.Error.WriteLine($"XML not found: {inputXmlPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Parse the XML file that contains video references.
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(inputXmlPath);

            // Expected XML format:
            // <Videos>
            //   <Video page="1" x="100" y="500" width="200" height="150" file="sample.mp4" />
            //   ...
            // </Videos>
            XmlNodeList videoNodes = xmlDoc.SelectNodes("//Video");
            if (videoNodes != null)
            {
                foreach (XmlNode node in videoNodes)
                {
                    // Extract attributes with fallback defaults.
                    int pageNumber = int.Parse(node.Attributes["page"]?.Value ?? "1");
                    float x        = float.Parse(node.Attributes["x"]?.Value ?? "0");
                    float y        = float.Parse(node.Attributes["y"]?.Value ?? "0");
                    float width    = float.Parse(node.Attributes["width"]?.Value ?? "100");
                    float height   = float.Parse(node.Attributes["height"]?.Value ?? "100");
                    string videoPath = node.Attributes["file"]?.Value;

                    if (string.IsNullOrEmpty(videoPath) || !File.Exists(videoPath))
                    {
                        Console.Error.WriteLine($"Video file not found: {videoPath}");
                        continue;
                    }

                    // Ensure the requested page exists (Aspose.Pdf uses 1‑based indexing).
                    if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Invalid page number {pageNumber} for video {videoPath}");
                        continue;
                    }

                    Page page = pdfDoc.Pages[pageNumber];

                    // Define the annotation rectangle (lower‑left and upper‑right corners).
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                        x,                     // llx
                        y,                     // lly
                        x + width,             // urx
                        y + height);           // ury

                    // Create a MovieAnnotation which embeds the video file.
                    // Constructor: MovieAnnotation(Page page, Rectangle rect, string movieFile)
                    MovieAnnotation movieAnn = new MovieAnnotation(page, rect, videoPath)
                    {
                        // Optional: set a title or contents for the annotation.
                        Title    = Path.GetFileNameWithoutExtension(videoPath),
                        Contents = $"Video: {Path.GetFileName(videoPath)}"
                    };

                    // Add the annotation to the page.
                    page.Annotations.Add(movieAnn);
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with embedded videos saved to '{outputPdfPath}'.");
    }
}