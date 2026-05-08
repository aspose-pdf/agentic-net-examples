using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string inputXmlPath   = "images.xml";
        const string outputPdfPath  = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(inputXmlPath))
        {
            Console.Error.WriteLine($"XML not found: {inputXmlPath}");
            return;
        }

        // Load the source PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load XML and extract all <svg> elements
            XDocument xmlDoc = XDocument.Load(inputXmlPath);
            var svgElements = xmlDoc.Descendants("svg");

            foreach (var svgElem in svgElements)
            {
                // Write SVG content to a temporary file
                string tempSvgPath = System.IO.Path.Combine(
                    System.IO.Path.GetTempPath(),
                    Guid.NewGuid().ToString() + ".svg");

                File.WriteAllText(tempSvgPath, svgElem.ToString());

                // Load the SVG as a PDF document (vector graphics are preserved)
                using (Document svgDoc = new Document(tempSvgPath, new SvgLoadOptions()))
                {
                    // Append all pages from the SVG document to the main PDF
                    pdfDoc.Pages.Add(svgDoc.Pages);
                }

                // Clean up the temporary file
                try { File.Delete(tempSvgPath); } catch { /* ignore */ }
            }

            // Save the resulting PDF with embedded vector graphics
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with embedded SVG graphics saved to '{outputPdfPath}'.");
    }
}