using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string tempXmlPath = "temp.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // 1. Export the current PDF structure to XML (optional – kept for
            //    demonstration purposes). The XML is not used for binding
            //    because the core Document API does not expose a BindXML
            //    method in recent versions of Aspose.PDF.
            // ------------------------------------------------------------
            pdfDoc.Save(tempXmlPath, new XmlSaveOptions());

            // Load the exported XML into an XmlDocument for manipulation
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(tempXmlPath);

            // Create an <HtmlFragment> element and wrap the HTML content inside a CDATA section
            XmlElement htmlFragment = xmlDoc.CreateElement("HtmlFragment");
            string htmlContent = "<p>This is <b>HTML</b> fragment.</p>";
            XmlCDataSection cdata = xmlDoc.CreateCDataSection(htmlContent);
            htmlFragment.AppendChild(cdata);

            // Append the new element to the root of the XML document
            XmlNode root = xmlDoc.DocumentElement;
            root?.AppendChild(htmlFragment);

            // Save the modified XML back to the temporary file (still optional)
            xmlDoc.Save(tempXmlPath);

            // ------------------------------------------------------------
            // 2. Insert the same HTML fragment directly into the PDF using the
            //    core HtmlFragment class. This avoids the need for a BindXML
            //    call, which is not part of the core API.
            // ------------------------------------------------------------
            HtmlFragment fragment = new HtmlFragment(htmlContent);
            // Add the fragment to the first page (or any page you prefer)
            pdfDoc.Pages[1].Paragraphs.Add(fragment);

            // Save the updated PDF
            pdfDoc.Save(outputPdfPath);
        }

        // Clean up the temporary XML file
        if (File.Exists(tempXmlPath))
        {
            try { File.Delete(tempXmlPath); } catch { }
        }

        Console.WriteLine($"PDF saved with HtmlFragment: {outputPdfPath}");
    }
}
