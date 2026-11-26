using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "data.xml";
        const string pdfTemplatePath = "template.pdf";
        const string outputPdfPath = "output.pdf";
        const string tempXmlPath = "temp_modified.xml"; // temporary file for the modified XML

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

        // Load the XML document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Locate the PageInfo element and set IsLandscape to true
        XmlNode pageInfoNode = xmlDoc.SelectSingleNode("//PageInfo");
        if (pageInfoNode?.Attributes != null)
        {
            XmlAttribute isLandscapeAttr = pageInfoNode.Attributes["IsLandscape"];
            if (isLandscapeAttr == null)
            {
                isLandscapeAttr = xmlDoc.CreateAttribute("IsLandscape");
                pageInfoNode.Attributes.Append(isLandscapeAttr);
            }
            isLandscapeAttr.Value = "true";
        }
        else
        {
            Console.Error.WriteLine("PageInfo element not found in XML.");
            return;
        }

        // Save the modified XML to a temporary file – BindXml expects a file path (string)
        xmlDoc.Save(tempXmlPath);

        // Load the PDF, bind the modified XML, and save the result
        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            pdfDoc.BindXml(tempXmlPath); // Bind the XML to the PDF using the temporary file path
            pdfDoc.Save(outputPdfPath);   // Save as PDF
        }

        // Clean up the temporary XML file
        try
        {
            File.Delete(tempXmlPath);
        }
        catch
        {
            // If deletion fails, ignore – the file is harmless.
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }
}
