using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // PDF with form fields
        const string outputXml = "form_data.xml";      // Desired XML output
        const string tempXml   = "temp_doc.xml";       // Temporary file for full PDF XML
        const string customRoot = "CustomFormRoot";    // External system required root name

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (core API, no Facades)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Export the whole PDF structure (including form fields) to an intermediate XML file
                pdfDoc.SaveXml(tempXml);
            }

            // Load the intermediate XML, rename the root element, and save to the final location
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(tempXml);

            // Preserve the original root's children
            XmlElement oldRoot = xmlDoc.DocumentElement;
            XmlElement newRoot = xmlDoc.CreateElement(customRoot);

            // Move all child nodes from the old root to the new root
            while (oldRoot.HasChildNodes)
            {
                XmlNode child = oldRoot.FirstChild;
                oldRoot.RemoveChild(child);
                newRoot.AppendChild(child);
            }

            // Replace the old root with the new custom root
            xmlDoc.ReplaceChild(newRoot, oldRoot);

            // Save the modified XML to the requested output file
            xmlDoc.Save(outputXml);

            // Clean up the temporary file
            File.Delete(tempXml);

            Console.WriteLine($"Form data exported to XML with custom root '{customRoot}' at '{outputXml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}