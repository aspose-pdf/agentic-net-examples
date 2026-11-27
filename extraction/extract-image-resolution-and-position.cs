using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text; // ImagePlacementAbsorber lives here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "image_metadata.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document has at least 9 pages (1‑based indexing)
            if (pdfDoc.Pages.Count < 9)
            {
                Console.Error.WriteLine("The document does not contain page 9.");
                return;
            }

            // Create the absorber that extracts image placement information
            ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

            // Apply the absorber to page 9
            pdfDoc.Pages[9].Accept(absorber);

            // Build an XML document with the collected metadata
            XDocument xmlDoc = new XDocument(new XElement("Images"));

            // Iterate over each image placement collected by the absorber
            foreach (ImagePlacement placement in absorber.ImagePlacements)
            {
                // Image resolution (DPI). "Resolution" is an Aspose.Pdf.Resolution object.
                // Use the horizontal DPI (X) – Y is typically the same for raster images.
                double resolutionDpi = placement.Resolution?.X ?? 0;

                // Rectangle describing the image position on the page
                Aspose.Pdf.Rectangle rect = placement.Rectangle;

                XElement imageElement = new XElement("Image",
                    new XAttribute("ResolutionDpi", resolutionDpi),
                    new XAttribute("LLX", rect.LLX),
                    new XAttribute("LLY", rect.LLY),
                    new XAttribute("URX", rect.URX),
                    new XAttribute("URY", rect.URY));

                xmlDoc.Root?.Add(imageElement);
            }

            // Save the XML metadata file
            xmlDoc.Save(outputXmlPath);
            Console.WriteLine($"Image metadata written to '{outputXmlPath}'.");
        }
    }
}
