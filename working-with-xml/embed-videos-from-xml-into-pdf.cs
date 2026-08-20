using System;
using System.Globalization;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the XML descriptor and the output PDF
        const string inputPdfPath = "input.pdf";
        const string xmlDescriptor = "videos.xml";
        const string outputPdfPath = "output.pdf";
        const string sampleVideoPath = "sample.mp4";

        // ------------------------------------------------------------
        // Ensure a placeholder PDF exists (self‑contained example)
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPdfPath);
            }
        }

        // ------------------------------------------------------------
        // Ensure a sample video file exists (any binary data is accepted)
        // ------------------------------------------------------------
        if (!File.Exists(sampleVideoPath))
        {
            // Write a few bytes – Aspose.Pdf only needs a stream, the content
            // does not have to be a valid video for the purpose of this demo.
            File.WriteAllBytes(sampleVideoPath, new byte[] { 0x00, 0x01, 0x02, 0x03 });
        }

        // ------------------------------------------------------------
        // Ensure a simple XML descriptor exists
        // ------------------------------------------------------------
        if (!File.Exists(xmlDescriptor))
        {
            var sampleXml =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                "<Videos>\n" +
                "  <Video page=\"1\" llx=\"100\" lly=\"500\" urx=\"300\" ury=\"700\" src=\"sample.mp4\" />\n" +
                "</Videos>";
            File.WriteAllText(xmlDescriptor, sampleXml);
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load and parse the XML file that contains video references
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlDescriptor);

            // Expected XML format:
            // <Videos>
            //   <Video page="1" llx="100" lly="500" urx="300" ury="700" src="video1.mp4" />
            //   ...
            // </Videos>
            XmlNodeList? videoNodes = xmlDoc.SelectNodes("//Video");
            if (videoNodes != null)
            {
                foreach (XmlNode node in videoNodes)
                {
                    // Safely retrieve attribute values
                    string? pageAttr = node.Attributes?["page"]?.Value;
                    string? llxAttr = node.Attributes?["llx"]?.Value;
                    string? llyAttr = node.Attributes?["lly"]?.Value;
                    string? urxAttr = node.Attributes?["urx"]?.Value;
                    string? uryAttr = node.Attributes?["ury"]?.Value;
                    string? srcAttr = node.Attributes?["src"]?.Value;

                    if (pageAttr == null || llxAttr == null || llyAttr == null ||
                        urxAttr == null || uryAttr == null || srcAttr == null)
                    {
                        Console.Error.WriteLine("One or more required attributes are missing for a <Video> node. Skipping.");
                        continue;
                    }

                    // Parse numeric attributes using invariant culture
                    if (!int.TryParse(pageAttr, NumberStyles.Integer, CultureInfo.InvariantCulture, out int pageNumber) ||
                        !double.TryParse(llxAttr, NumberStyles.Float, CultureInfo.InvariantCulture, out double llx) ||
                        !double.TryParse(llyAttr, NumberStyles.Float, CultureInfo.InvariantCulture, out double lly) ||
                        !double.TryParse(urxAttr, NumberStyles.Float, CultureInfo.InvariantCulture, out double urx) ||
                        !double.TryParse(uryAttr, NumberStyles.Float, CultureInfo.InvariantCulture, out double ury))
                    {
                        Console.Error.WriteLine("Failed to parse numeric attributes for a <Video> node. Skipping.");
                        continue;
                    }

                    string videoFilePath = srcAttr;

                    // Ensure the referenced video file exists (create a placeholder if not)
                    if (!File.Exists(videoFilePath))
                    {
                        Console.Error.WriteLine($"Video file not found: {videoFilePath}. Creating a dummy placeholder.");
                        File.WriteAllBytes(videoFilePath, new byte[] { 0x00, 0x01, 0x02, 0x03 });
                    }

                    // Get the target page (Aspose.Pdf uses 1‑based indexing)
                    if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Invalid page number {pageNumber}. Skipping this video.");
                        continue;
                    }
                    Page targetPage = pdfDoc.Pages[pageNumber];

                    // Define the annotation rectangle
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                    // Create the RichMediaAnnotation and specify that the content is a video
                    RichMediaAnnotation richMedia = new RichMediaAnnotation(targetPage, rect)
                    {
                        Type = RichMediaAnnotation.ContentType.Video
                    };

                    // Attach the video stream to the annotation. The first parameter is the
                    // optional name of the media; an empty string satisfies the non‑nullable
                    // requirement without affecting functionality.
                    using (FileStream videoStream = File.OpenRead(videoFilePath))
                    {
                        richMedia.SetContent(string.Empty, videoStream);
                    }

                    // Add the annotation to the page
                    targetPage.Annotations.Add(richMedia);
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with embedded videos saved to '{outputPdfPath}'.");
    }
}
