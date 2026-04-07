using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class SvgXmlToPdfRenderer
{
    static void Main()
    {
        const string xmlInputPath = "input.xml";          // XML containing embedded <svg> elements
        const string pdfOutputPath = "output.pdf";        // Resulting PDF file

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        // Load the XML document
        XDocument xDoc = XDocument.Load(xmlInputPath);

        // Create the target PDF document
        using (Document pdfDoc = new Document())
        {
            // Find all <svg> elements (ignoring namespaces)
            foreach (XElement svgElement in xDoc.Descendants())
            {
                if (svgElement.Name.LocalName.Equals("svg", StringComparison.OrdinalIgnoreCase))
                {
                    // Convert the SVG element (including its children) to a string
                    string svgContent = svgElement.ToString();

                    // Load the SVG string into a temporary PDF document using SvgLoadOptions
                    using (MemoryStream svgStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(svgContent)))
                    using (Document svgDoc = new Document(svgStream, new SvgLoadOptions()))
                    {
                        // Append all pages from the SVG-derived document to the target PDF
                        pdfDoc.Pages.Add(svgDoc.Pages);
                    }
                }
            }

            // Save the assembled PDF
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"SVG images from XML have been rendered into PDF: {pdfOutputPath}");
    }
}