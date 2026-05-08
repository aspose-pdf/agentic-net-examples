using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source XML and the resulting PDF
        const string xmlPath = "input.xml";
        const string outputPdf = "output.pdf";

        // ---------------------------------------------------------------------
        // Ensure the XML file exists. If it does not, create a minimal in‑memory
        // example so the program can continue without throwing FileNotFound.
        // ---------------------------------------------------------------------
        XDocument xdoc;
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML file '{xmlPath}' not found – creating a default example.");
            // Create a simple XML with a single <Video> element. The video file
            // reference is also optional – the code later validates its existence.
            xdoc = new XDocument(
                new XElement("Videos",
                    new XElement("Video",
                        new XAttribute("page", 1),
                        new XAttribute("x", 100),
                        new XAttribute("y", 100),
                        new XAttribute("width", 200),
                        new XAttribute("height", 150),
                        new XAttribute("file", "sample.mp4")
                    )
                )
            );
        }
        else
        {
            // Load the XML that contains video references
            xdoc = XDocument.Load(xmlPath);
        }

        // Guard against a missing root element
        if (xdoc.Root == null)
        {
            Console.WriteLine("The XML document does not contain a root element.");
            return;
        }

        // Create a new PDF document (empty) and ensure at least one page exists
        using (Document pdfDoc = new Document())
        {
            // Add a blank page – additional pages will be added as needed later
            pdfDoc.Pages.Add();

            // Iterate over each <Video> element in the XML (any name under the root)
            foreach (XElement videoElem in xdoc.Root.Elements("Video"))
            {
                // ----- Safely read attributes (null‑check + default values) -----
                int pageNumber = (int?)videoElem.Attribute("page") ?? 1; // default to first page
                double x = (double?)videoElem.Attribute("x") ?? 0.0;
                double y = (double?)videoElem.Attribute("y") ?? 0.0;
                double width = (double?)videoElem.Attribute("width") ?? 100.0;   // arbitrary default
                double height = (double?)videoElem.Attribute("height") ?? 100.0; // arbitrary default
                string? videoFile = (string?)videoElem.Attribute("file");

                // Validate required data
                if (string.IsNullOrWhiteSpace(videoFile))
                {
                    Console.WriteLine("Skipping a <Video> element because the 'file' attribute is missing or empty.");
                    continue;
                }
                if (!File.Exists(videoFile))
                {
                    Console.WriteLine($"Video file '{videoFile}' not found. Skipping this element.");
                    continue;
                }

                // Ensure the requested page exists; add blank pages if necessary
                while (pdfDoc.Pages.Count < pageNumber)
                {
                    pdfDoc.Pages.Add();
                }

                // Get the target page (1‑based index)
                Page targetPage = pdfDoc.Pages[pageNumber];

                // Define the annotation rectangle (lower‑left to upper‑right)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    x,                 // llx
                    y,                 // lly
                    x + width,         // urx
                    y + height);       // ury

                // Create the RichMediaAnnotation on the page
                RichMediaAnnotation richMedia = new RichMediaAnnotation(targetPage, rect);
                // Specify that the embedded content is a video (ContentType enum is optional – SetContent infers it)
                richMedia.Type = RichMediaAnnotation.ContentType.Video;

                // Embed the video file into the annotation
                using (FileStream videoStream = File.OpenRead(videoFile))
                {
                    // The first argument is a name identifier for the content
                    richMedia.SetContent(Path.GetFileName(videoFile), videoStream);
                }

                // Add the annotation to the page
                targetPage.Annotations.Add(richMedia);
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with embedded videos saved to '{outputPdf}'.");
    }
}
