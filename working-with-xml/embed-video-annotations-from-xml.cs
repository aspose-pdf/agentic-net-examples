using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";      // XML describing video locations
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document (no special load options needed for XML parsing)
        XDocument xDoc = XDocument.Load(xmlPath);

        // Create a new PDF document (empty) – you can also load a template PDF if required
        using (Document pdfDoc = new Document())
        {
            // Ensure at least one page exists to place annotations
            if (pdfDoc.Pages.Count == 0)
                pdfDoc.Pages.Add();

            // Iterate over each <Video> element in the XML
            foreach (XElement videoElem in xDoc.Descendants("Video"))
            {
                // Expected attributes: page, x, y, width, height, file
                int pageNumber = (int?)videoElem.Attribute("page") ?? 1;
                double llx = (double?)videoElem.Attribute("x") ?? 0;
                double lly = (double?)videoElem.Attribute("y") ?? 0;
                double width = (double?)videoElem.Attribute("width") ?? 100;
                double height = (double?)videoElem.Attribute("height") ?? 100;
                double urx = llx + width;
                double ury = lly + height;
                string videoPath = (string)videoElem.Attribute("file");

                if (!File.Exists(videoPath))
                {
                    Console.Error.WriteLine($"Video file not found: {videoPath}");
                    continue;
                }

                // Ensure the target page exists (Aspose.Pdf uses 1‑based indexing)
                while (pdfDoc.Pages.Count < pageNumber)
                    pdfDoc.Pages.Add();

                Page page = pdfDoc.Pages[pageNumber];

                // Create the RichMediaAnnotation
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
                RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
                {
                    // Set the annotation type to Video
                    Type = RichMediaAnnotation.ContentType.Video,
                    // Optional visual appearance
                    Color = Aspose.Pdf.Color.LightGray
                };

                // Embed the video content
                using (FileStream videoStream = File.OpenRead(videoPath))
                {
                    // The first parameter is a name for the content stream; can be any string
                    richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
                }

                // Add the annotation to the page
                page.Annotations.Add(richMedia);
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with embedded videos saved to '{outputPdf}'.");
    }
}
