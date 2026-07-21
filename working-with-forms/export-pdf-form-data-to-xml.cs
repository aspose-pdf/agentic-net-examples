using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlOutputPath = "formdata.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Create an XML document to hold the field data
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("FormData");
            xmlDoc.AppendChild(root);

            // Iterate over all form fields using the Fields collection
            foreach (Field field in doc.Form.Fields)
            {
                // Field name (partial name) and its current value
                string fieldName = field.PartialName;
                string fieldValue = field.Value?.ToString() ?? string.Empty;

                XmlElement fieldElement = xmlDoc.CreateElement("Field");
                fieldElement.SetAttribute("name", fieldName);
                fieldElement.InnerText = fieldValue;
                root.AppendChild(fieldElement);
            }

            // Save the XML document to the specified path
            xmlDoc.Save(xmlOutputPath);
        }

        Console.WriteLine($"Form data successfully saved to '{xmlOutputPath}'.");
    }
}
