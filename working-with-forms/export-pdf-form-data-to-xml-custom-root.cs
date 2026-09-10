using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExportFormDataToCustomXml
{
    static void Main()
    {
        // Input PDF containing a form.
        const string inputPdfPath = "input_form.pdf";

        // Desired output XML file path.
        const string outputXmlPath = "form_data_custom.xml";

        // Custom root element name required by the external system.
        const string customRootName = "CustomFormData";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Access the XFA form (if present). XFA holds the form data as XML.
            XFA xfa = pdfDoc.Form.XFA;

            // XFA.Datasets contains the data part of the XFA form.
            // Load it into an XmlDocument for manipulation.
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xfa.Datasets.OuterXml);

            // Rename the root element to the custom name.
            XmlElement oldRoot = xmlDoc.DocumentElement;
            XmlElement newRoot = xmlDoc.CreateElement(customRootName);

            // Transfer all child nodes from the old root to the new root.
            while (oldRoot.HasChildNodes)
            {
                XmlNode child = oldRoot.FirstChild;
                oldRoot.RemoveChild(child);
                newRoot.AppendChild(child);
            }

            // Replace the old root with the new one.
            xmlDoc.ReplaceChild(newRoot, oldRoot);

            // Save the modified XML to the desired file.
            xmlDoc.Save(outputXmlPath);
        }

        Console.WriteLine($"Form data exported to XML with custom root '{customRootName}': {outputXmlPath}");
    }
}