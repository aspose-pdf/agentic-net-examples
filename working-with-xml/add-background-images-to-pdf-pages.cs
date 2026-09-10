using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for BackgroundArtifact (inherits from Artifact)

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string xmlPath   = "backgrounds.xml";
        const string outputPdf = "output_branded.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load XML that defines background images per page.
        // Expected format:
        // <Backgrounds>
        //   <Page number="1" image="bg1.png" />
        //   <Page number="2" image="bg2.png" />
        //   ...
        // </Backgrounds>
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Open the PDF document.
        using (Document doc = new Document(pdfPath))
        {
            // Iterate over all pages (1‑based indexing).
            foreach (Page page in doc.Pages)
            {
                // Find a matching <Page> element in the XML.
                XElement bgElement = null;
                foreach (XElement el in xmlDoc.Root.Elements("Page"))
                {
                    XAttribute numAttr = el.Attribute("number");
                    if (numAttr != null && int.TryParse(numAttr.Value, out int num) && num == page.Number)
                    {
                        bgElement = el;
                        break;
                    }
                }

                // If no background defined for this page, continue.
                if (bgElement == null) continue;

                // Get the image file path.
                XAttribute imgAttr = bgElement.Attribute("image");
                if (imgAttr == null) continue;
                string imagePath = imgAttr.Value;

                if (!File.Exists(imagePath))
                {
                    Console.Error.WriteLine($"Background image not found: {imagePath} (page {page.Number})");
                    continue;
                }

                // Create a background artifact, set the image, and mark it as background.
                BackgroundArtifact bgArtifact = new BackgroundArtifact();
                bgArtifact.IsBackground = true;          // place behind page content
                bgArtifact.SetImage(imagePath);          // load image from file

                // Add the artifact to the page.
                page.Artifacts.Add(bgArtifact);
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Branded PDF saved to '{outputPdf}'.");
    }
}