using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlInputPath = "input.xml";   // XML containing <svg> elements
        const string pdfOutputPath = "output.pdf"; // Resulting PDF with embedded vector graphics

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        // Load the XML document and select all <svg> elements (any namespace)
        XDocument xDoc = XDocument.Load(xmlInputPath);
        var svgElements = xDoc.Descendants()
                              .Where(e => e.Name.LocalName.Equals("svg", StringComparison.OrdinalIgnoreCase));

        // Create the target PDF document
        using (Document targetPdf = new Document())
        {
            foreach (var svgElem in svgElements)
            {
                // Serialize the <svg> element (including its children) to a UTF‑8 string
                string svgString = svgElem.ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
                byte[] svgBytes = Encoding.UTF8.GetBytes(svgString);

                // Load the SVG string as a PDF document (each SVG becomes one page)
                using (MemoryStream svgStream = new MemoryStream(svgBytes))
                using (Document svgDoc = new Document(svgStream, new SvgLoadOptions()))
                {
                    // Append all pages from the SVG document to the target PDF
                    foreach (Page page in svgDoc.Pages)
                    {
                        targetPdf.Pages.Add(page);
                    }
                }
            }

            // Save the combined PDF
            targetPdf.Save(pdfOutputPath, new PdfSaveOptions());
            Console.WriteLine($"PDF with embedded SVG graphics saved to '{pdfOutputPath}'.");
        }
    }
}