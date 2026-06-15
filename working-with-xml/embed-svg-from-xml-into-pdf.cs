using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML (containing SVG fragments) and the PDF template.
        const string xmlPath = "input.xml";
        const string pdfTemplatePath = "template.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }

        // Load the XML document and select all <svg> elements.
        // The SVG namespace is optional; handle both namespaced and non‑namespaced cases.
        XDocument xDoc = XDocument.Load(xmlPath);
        var svgElements = xDoc.Descendants("{http://www.w3.org/2000/svg}svg");
        if (!svgElements.Any())
        {
            // Fallback to elements without a namespace.
            svgElements = xDoc.Descendants("svg");
        }

        // Open the PDF document inside a using block (ensures proper disposal).
        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            int pageNumber = 1; // Start embedding on the first page.

            foreach (var svgElement in svgElements)
            {
                // Ensure the target page exists; add new pages if necessary.
                if (pageNumber > pdfDoc.Pages.Count)
                {
                    pdfDoc.Pages.Add();
                }

                Page page = pdfDoc.Pages[pageNumber];

                // Convert the SVG element to a UTF‑8 byte array.
                byte[] svgBytes = System.Text.Encoding.UTF8.GetBytes(svgElement.ToString());

                // Add the SVG as a vector image to the page.
                // The AddImage overload that accepts a Stream treats SVG content as vector graphics.
                using (MemoryStream svgStream = new MemoryStream(svgBytes))
                {
                    // Define the rectangle where the SVG will be placed.
                    // Adjust coordinates as needed (left, bottom, right, top).
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 500, 550, 800);

                    // Embed the SVG; it will be rendered as vector graphics.
                    page.AddImage(svgStream, rect);
                }

                pageNumber++; // Move to the next page for the next SVG fragment.
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with embedded SVG graphics saved to '{outputPdfPath}'.");
    }
}