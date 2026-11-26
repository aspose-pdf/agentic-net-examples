using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "template.pdf";
        const string xmlDataPath      = "data.xml";
        const string outputPdfPath    = "output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Load the PDF document (must be disposed via using)
        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            // Load the existing XML data
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlDataPath);

            // Create a new <Graph> element
            XmlElement graphElem = xmlDoc.CreateElement("Graph");

            // Create a <Circle> child element and set its attributes (example attributes)
            XmlElement circleElem = xmlDoc.CreateElement("Circle");
            circleElem.SetAttribute("Radius", "50");
            circleElem.SetAttribute("CenterX", "100");
            circleElem.SetAttribute("CenterY", "150");
            graphElem.AppendChild(circleElem);

            // Create a <GraphInfo> child element and set its inner text (example content)
            XmlElement graphInfoElem = xmlDoc.CreateElement("GraphInfo");
            graphInfoElem.InnerText = "Sample graph generated programmatically.";
            graphElem.AppendChild(graphInfoElem);

            // Append the new <Graph> element to the root of the XML document
            XmlElement root = xmlDoc.DocumentElement;
            if (root == null)
            {
                Console.Error.WriteLine("XML document has no root element.");
                return;
            }
            root.AppendChild(graphElem);

            // ---------------------------------------------------------------------
            // NOTE:
            // The original code attempted to call Document.BindXML(string) to bind the
            // modified XML to the PDF. In recent versions of Aspose.PDF (26.x) the
            // BindXML method has been removed from the core Document API. The recommended
            // approach is to embed the XML as an attached file (EmbeddedFiles) and, if
            // required, convert the PDF to a PDF/A‑3B document for ZUGFeRD‑style usage.
            // ---------------------------------------------------------------------

            // Save the modified XML to a temporary file
            string tempXmlPath = Path.GetTempFileName();
            try
            {
                xmlDoc.Save(tempXmlPath);

                // Embed the XML into the PDF as an attached file (so the data is still
                // available inside the PDF). This replaces the old BindXML call.
                using (var xmlStream = File.OpenRead(tempXmlPath))
                {
                    var spec = new FileSpecification(xmlStream, Path.GetFileName(xmlDataPath), "Embedded XML data")
                    {
                        MIMEType = "text/xml",
                        AFRelationship = AFRelationship.Data
                    };
                    pdfDoc.EmbeddedFiles.Add(spec);
                }
            }
            finally
            {
                // Clean up the temporary file
                if (File.Exists(tempXmlPath))
                    File.Delete(tempXmlPath);
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with added Graph element saved to '{outputPdfPath}'.");
    }
}
