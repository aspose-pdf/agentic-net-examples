using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // source PDF with form fields
        const string xmlPath   = "data.xml";    // XML file containing field values
        const string outputPath = "output.pdf"; // PDF after importing data

        // Verify that both source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Load the XML document containing the form data
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // Optional: improve performance when many fields are present
            pdfDoc.Form.AutoRecalculate = false;

            // Iterate over each form field in the PDF
            foreach (Field field in pdfDoc.Form.Fields)
            {
                // Attempt to locate an XML element whose name matches the field name
                // Adjust the XPath if your XML structure nests fields differently
                XmlNode node = xmlDoc.SelectSingleNode($"//{field.Name}");
                if (node != null)
                {
                    // Update the field value with the XML element's inner text
                    field.Value = node.InnerText;
                }
            }

            // Save the updated PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Form data imported from '{xmlPath}' and saved to '{outputPath}'.");
    }
}