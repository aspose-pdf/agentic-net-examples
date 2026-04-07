using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF, XML describing backgrounds, and output PDF paths
        const string pdfPath   = "input.pdf";
        const string xmlPath   = "backgrounds.xml";
        const string outputPdf = "output.pdf";

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

        // Load the XML that maps page numbers to background image files.
        // Expected format:
        // <Backgrounds>
        //   <Page number="1" image="bg1.png" />
        //   <Page number="2" image="bg2.png" />
        //   ...
        // </Backgrounds>
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over all pages (1‑based indexing).
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Find the corresponding <Page> element in the XML.
                // xmlDoc.Root is the <Backgrounds> element, so we can query its children directly.
                XElement bgElement = xmlDoc.Root?
                    .Elements("Page")
                    .FirstOrDefault(e => (int?)e.Attribute("number") == page.Number);

                if (bgElement == null)
                    continue; // No background defined for this page.

                string imagePath = (string)bgElement.Attribute("image");
                if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
                    continue; // Skip if image file is missing.

                // Set the background image for the page.
                // Page.BackgroundImage expects an Aspose.Pdf.Image instance.
                Aspose.Pdf.Image bgImage = new Aspose.Pdf.Image();
                bgImage.File = imagePath;
                page.BackgroundImage = bgImage;
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Background images applied. Saved to '{outputPdf}'.");
    }
}
