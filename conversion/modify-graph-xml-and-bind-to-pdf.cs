using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;               // Core PDF API (Document, BindXml, etc.)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath          = "data.xml";          // Source XML file
        const string pdfTemplatePath  = "template.pdf";      // PDF containing a Graph element
        const string outputPdfPath    = "result.pdf";        // Destination PDF
        const string targetGraphId    = "graph1";            // Id of the Graph element to modify

        // -----------------------------------------------------------------
        // 1. Load and modify the XML
        // -----------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Locate the <Graph> element with the specified Id attribute
        XElement graphElement = xmlDoc
            .Descendants("Graph")
            .FirstOrDefault(e => (string)e.Attribute("Id") == targetGraphId);

        if (graphElement == null)
        {
            Console.Error.WriteLine($"Graph element with Id='{targetGraphId}' not found in XML.");
            return;
        }

        // Modify GraphInfo properties – represented as child elements in the XML
        // Adjust the element names according to your XML schema if they differ.
        graphElement.SetElementValue("Title", "Updated Graph Title");
        graphElement.SetElementValue("XAxisLabel", "New X‑Axis");
        graphElement.SetElementValue("YAxisLabel", "New Y‑Axis");
        graphElement.SetElementValue("Data", "10,20,30,40");   // Example data series

        // Save the modified XML to a temporary file for binding
        string modifiedXmlPath = Path.Combine(Path.GetTempPath(), $"modified_{Guid.NewGuid()}.xml");
        xmlDoc.Save(modifiedXmlPath);

        // -----------------------------------------------------------------
        // 2. Load the PDF template, bind the modified XML, and save the result
        // -----------------------------------------------------------------
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }

        // Document disposal is handled via a using block (lifecycle rule)
        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            // Bind the XML data to the PDF. This populates form fields and Graph elements.
            pdfDoc.BindXml(modifiedXmlPath);

            // Save the final PDF. No SaveOptions needed because the target format is PDF.
            pdfDoc.Save(outputPdfPath);
        }

        // Clean up the temporary XML file
        try { File.Delete(modifiedXmlPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"PDF generated successfully: {outputPdfPath}");
    }
}