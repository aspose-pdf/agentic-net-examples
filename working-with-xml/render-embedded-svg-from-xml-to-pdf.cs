using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Aspose.Pdf; // SvgLoadOptions, Document, etc.

class Program
{
    static void Main()
    {
        const string xmlInputPath  = "input.xml";      // XML containing embedded <svg> elements
        const string pdfOutputPath = "output.pdf";     // Resulting PDF with rendered SVGs

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"Input XML not found: {xmlInputPath}");
            return;
        }

        try
        {
            // Load the XML document
            XDocument xDoc = XDocument.Load(xmlInputPath);

            // Create an empty PDF document that will receive the rendered SVG pages
            using (Document pdfDoc = new Document())
            {
                // Iterate over all <svg> elements in the XML (any namespace or the default SVG namespace)
                foreach (XElement svgElement in xDoc.Descendants())
                {
                    if (svgElement.Name.LocalName.Equals("svg", StringComparison.OrdinalIgnoreCase))
                    {
                        // Serialize the <svg> element (including its children) to a UTF‑8 string
                        string svgContent = svgElement.ToString(System.Xml.Linq.SaveOptions.DisableFormatting);

                        // Convert the SVG string to a memory stream
                        byte[] svgBytes = Encoding.UTF8.GetBytes(svgContent);
                        using (MemoryStream svgStream = new MemoryStream(svgBytes))
                        {
                            // Load the SVG as a PDF document using SvgLoadOptions
                            using (Document svgDoc = new Document(svgStream, new SvgLoadOptions()))
                            {
                                // Append all pages from the SVG‑derived document into the target PDF
                                pdfDoc.Pages.Add(svgDoc.Pages);
                            }
                        }
                    }
                }

                // Save the combined PDF
                pdfDoc.Save(pdfOutputPath);
            }

            Console.WriteLine($"SVG images from '{xmlInputPath}' have been rendered into '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
