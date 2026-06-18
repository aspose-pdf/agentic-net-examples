using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";   // source PDF
        const string xmlPath = "audio.xml";   // XML describing audio clips
        const string outputPath = "output.pdf"; // result PDF

        if (!File.Exists(pdfPath) || !File.Exists(xmlPath))
        {
            Console.Error.WriteLine("Input PDF or XML file not found.");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(pdfPath))
        {
            // Load and parse the XML file
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // Expected XML format:
            // <Audio page="1" x="100" y="500" width="20" height="20" file="sound.wav" />
            XmlNodeList audioNodes = xmlDoc.SelectNodes("//Audio");
            foreach (XmlNode node in audioNodes)
            {
                // Safely extract attributes (null‑check to silence CS8600/CS8602 warnings)
                if (node.Attributes == null) continue;
                var pageAttr = node.Attributes["page"]?.Value;
                var xAttr = node.Attributes["x"]?.Value;
                var yAttr = node.Attributes["y"]?.Value;
                var wAttr = node.Attributes["width"]?.Value;
                var hAttr = node.Attributes["height"]?.Value;
                var fileAttr = node.Attributes["file"]?.Value;
                if (pageAttr == null || xAttr == null || yAttr == null || wAttr == null || hAttr == null || fileAttr == null)
                    continue; // skip malformed entries

                // Parse numeric values
                if (!int.TryParse(pageAttr, out int pageNum) ||
                    !double.TryParse(xAttr, out double x) ||
                    !double.TryParse(yAttr, out double y) ||
                    !double.TryParse(wAttr, out double w) ||
                    !double.TryParse(hAttr, out double h))
                {
                    continue; // skip entries with invalid numbers
                }

                // Validate page number
                if (pageNum < 1 || pageNum > doc.Pages.Count)
                    continue; // skip invalid entries

                Page page = doc.Pages[pageNum];

                // Define the annotation rectangle (lower‑left and upper‑right corners)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(x, y, x + w, y + h);

                // Create a sound annotation that references the audio file.
                // The constructor (Page, Rectangle, string) is valid for recent Aspose.PDF versions.
                SoundAnnotation soundAnn = new SoundAnnotation(page, rect, fileAttr);

                // NOTE: In recent Aspose.PDF versions the IconEnum does not exist.
                // If a visual icon is required, the default speaker icon is used.
                // The following line is intentionally omitted to avoid CS0117.
                // soundAnn.Icon = SoundAnnotation.IconEnum.Speaker;

                // Add the annotation to the page
                page.Annotations.Add(soundAnn);
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Audio‑annotated PDF saved to '{outputPath}'.");
    }
}
