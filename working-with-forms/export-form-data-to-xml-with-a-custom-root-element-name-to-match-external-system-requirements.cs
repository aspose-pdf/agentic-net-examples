using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "output.xml";
        const string customRootName = "CustomFormData";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an XML document with the required custom root element
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement(customRootName);
            xmlDoc.AppendChild(root);

            // Iterate over all form fields and export name/value pairs
            Form form = pdfDoc.Form;
            foreach (Field field in form.Fields)
            {
                // Use FullName if available, otherwise Name; fallback to a placeholder
                string fieldName = field.FullName ?? field.Name ?? "UnnamedField";

                // Convert the field value to string, handling nulls
                string fieldValue = field.Value?.ToString() ?? string.Empty;

                // Create an element for this field and set its inner text
                XmlElement fieldElement = xmlDoc.CreateElement(XmlConvert.EncodeName(fieldName));
                fieldElement.InnerText = fieldValue;
                root.AppendChild(fieldElement);
            }

            // Save the XML document to file (lifecycle: save)
            xmlDoc.Save(outputXmlPath);
        }

        Console.WriteLine($"Form data exported to XML with root element '{customRootName}'.");
    }
}
