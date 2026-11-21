using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath   = "input.xml";      // XML containing <svg> elements
        const string outputPdf = "output.pdf";     // Resulting PDF with vector graphics

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Select all <svg> nodes (namespace‑agnostic)
        XmlNodeList svgNodes = xmlDoc.GetElementsByTagName("svg");
        if (svgNodes.Count == 0)
        {
            Console.WriteLine("No SVG elements found in the XML.");
            return;
        }

        // Create an empty PDF document that will receive the rendered SVG pages
        using (Document pdfDoc = new Document())
        {
            // Iterate over each SVG node
            foreach (XmlNode svgNode in svgNodes)
            {
                // Serialize the SVG node to a UTF‑8 byte array
                string svgXml = svgNode.OuterXml;
                byte[] svgBytes = System.Text.Encoding.UTF8.GetBytes(svgXml);

                // Load the SVG as a temporary Aspose.Pdf Document (vector graphics are preserved)
                using (MemoryStream svgStream = new MemoryStream(svgBytes))
                using (Document svgDoc = new Document(svgStream, new SvgLoadOptions()))
                {
                    // Append all pages from the SVG document to the target PDF
                    // Aspose.Pdf uses 1‑based page indexing (see global rule)
                    for (int i = 1; i <= svgDoc.Pages.Count; i++)
                    {
                        pdfDoc.Pages.Add(svgDoc.Pages[i]);
                    }
                }
            }

            // Save the combined PDF (no SaveOptions needed because the output is PDF)
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"SVG images rendered and saved to '{outputPdf}'.");
    }
}