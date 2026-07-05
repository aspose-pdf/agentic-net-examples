using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class ExportFormDataToXml
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF with form fields
        const string tempXmlPath = "temp_form_data.xml"; // intermediate XML file
        const string outputXmlPath = "form_data_custom.xml"; // final XML with custom root
        const string customRootName = "ExternalSystemFormData"; // required root element name

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export the form data (including XFA if present) to XML.
            // Non‑PDF format requires explicit SaveOptions (rule: save-to-non-pdf-always-use-save-options).
            XmlSaveOptions xmlOptions = new XmlSaveOptions();
            pdfDoc.Save(tempXmlPath, xmlOptions);
        }

        // Load the generated XML, rename the root element, and save to the final location.
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(tempXmlPath);

        // Preserve the original root's child nodes.
        XmlNode oldRoot = xmlDoc.DocumentElement;
        XmlElement newRoot = xmlDoc.CreateElement(customRootName);

        // Import all child nodes from the old root into the new root.
        foreach (XmlNode child in oldRoot.ChildNodes)
        {
            XmlNode imported = xmlDoc.ImportNode(child, true);
            newRoot.AppendChild(imported);
        }

        // Replace the old root with the new custom root.
        xmlDoc.ReplaceChild(newRoot, oldRoot);

        // Save the transformed XML to the desired output file.
        xmlDoc.Save(outputXmlPath);

        // Clean up the temporary file.
        try { File.Delete(tempXmlPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Form data exported to XML with custom root '{customRootName}': {outputXmlPath}");
    }
}