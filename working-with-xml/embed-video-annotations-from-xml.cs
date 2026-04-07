using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";          // XML describing videos
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML that contains video references
        XDocument xDoc = XDocument.Load(xmlPath);

        // Load (or create) a PDF document from the XML using XmlLoadOptions
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Iterate over each <Video> element in the XML
            foreach (XElement videoElem in xDoc.Root.Elements("Video"))
            {
                // Extract required data
                string videoPath = videoElem.Element("FilePath")?.Value;
                int pageNumber = int.Parse(videoElem.Element("Page")?.Value ?? "1");

                XElement rectElem = videoElem.Element("Rect");
                double llx = double.Parse(rectElem.Attribute("llx")?.Value ?? "0");
                double lly = double.Parse(rectElem.Attribute("lly")?.Value ?? "0");
                double urx = double.Parse(rectElem.Attribute("urx")?.Value ?? "100");
                double ury = double.Parse(rectElem.Attribute("ury")?.Value ?? "100");

                // Validate video file existence
                if (string.IsNullOrEmpty(videoPath) || !File.Exists(videoPath))
                {
                    Console.Error.WriteLine($"Video file not found: {videoPath}");
                    continue;
                }

                // Ensure the requested page exists (Aspose.Pdf uses 1‑based indexing)
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} for video {videoPath}");
                    continue;
                }

                Page targetPage = pdfDoc.Pages[pageNumber];

                // Create a rectangle for the annotation
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Use MovieAnnotation (simpler than RichMediaAnnotation) to embed the video
                MovieAnnotation movieAnn = new MovieAnnotation(targetPage, rect, videoPath)
                {
                    // Optional: set a title or contents for the annotation
                    Title = Path.GetFileName(videoPath),
                    Contents = "Click to play video"
                };

                // Add the annotation to the page
                targetPage.Annotations.Add(movieAnn);
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with embedded videos saved to '{outputPdf}'.");
    }
}