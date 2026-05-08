using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class ExportFormDataToXml
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdfPath = "form.pdf";

        // Desired output XML file
        const string outputXmlPath = "formData.xml";

        // Custom root element name required by the external system
        const string customRootName = "ExternalSystemFormData";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (using the standard Document constructor)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize XML save options – no RootElementName property in the current
            // Aspose.Pdf version, so we will rename the root element after the file is saved.
            XmlSaveOptions xmlOptions = new XmlSaveOptions();

            // Save the PDF content to a temporary XML file first.
            string tempXmlPath = Path.GetTempFileName();
            pdfDoc.Save(tempXmlPath, xmlOptions);

            // Load the generated XML, replace the root element name, and write to the final path.
            XDocument xDoc = XDocument.Load(tempXmlPath);
            XElement newRoot = new XElement(customRootName, xDoc.Root.Elements());
            XDocument finalDoc = new XDocument(newRoot);
            finalDoc.Save(outputXmlPath);

            // Clean up the temporary file.
            File.Delete(tempXmlPath);
        }

        Console.WriteLine($"Form data exported to XML with root '{customRootName}' at '{outputXmlPath}'.");
    }
}
