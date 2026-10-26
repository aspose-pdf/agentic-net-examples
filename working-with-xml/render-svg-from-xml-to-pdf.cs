using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath      = "input.xml";      // XML containing <svg> elements
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Create the target PDF that will hold all rendered SVGs
        using (Document targetPdf = new Document())
        {
            // Iterate over each <svg> element in the XML (1‑based page indexing rule applies when we later add pages)
            foreach (XElement svgElement in xmlDoc.Descendants("svg"))
            {
                // Serialize the SVG element to a memory stream
                using (MemoryStream svgStream = new MemoryStream())
                {
                    // Write the SVG XML (including the root <svg> tag) to the stream
                    svgElement.Save(svgStream);
                    svgStream.Position = 0; // Reset for reading

                    // Load the SVG as a temporary PDF document (vector graphics are preserved)
                    SvgLoadOptions loadOpts = new SvgLoadOptions(); // No GDI+ dependency
                    using (Document svgPdf = new Document(svgStream, loadOpts))
                    {
                        // Append all pages from the SVG‑derived PDF into the target PDF
                        targetPdf.Pages.Add(svgPdf.Pages);
                    }
                }
            }

            // Save the combined PDF (no special SaveOptions needed for PDF output)
            targetPdf.Save(outputPdfPath);
        }

        Console.WriteLine($"SVG images rendered as vector graphics and saved to '{outputPdfPath}'.");
    }
}