using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // PDF with form fields
        const string xmlPath = "data.xml";           // XML containing a subset of field values
        const string outputPath = "updated.pdf";     // Resulting PDF

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

        // Load the PDF document (core API)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Load the XML data into an XmlDocument for easy traversal
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // Assume each direct child element of the root represents a form field:
            // <Root>
            //   <FieldName1>Value1</FieldName1>
            //   <FieldName2>Value2</FieldName2>
            // </Root>
            XmlElement? root = xmlDoc.DocumentElement;
            if (root != null)
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    // Skip non‑element nodes (e.g., comments, text)
                    if (node.NodeType != XmlNodeType.Element) continue;

                    string fieldName = node.Name;          // XML element name = PDF field name
                    string fieldValue = node.InnerText;    // Value to set

                    // Check if the PDF contains a field with this name
                    if (pdfDoc.Form.HasField(fieldName))
                    {
                        // The Form indexer returns a WidgetAnnotation; cast it to Field
                        Field? field = pdfDoc.Form[fieldName] as Field;
                        if (field != null)
                        {
                            field.Value = fieldValue;
                        }
                    }
                }
            }

            // Save the updated PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF updated and saved to '{outputPath}'.");
    }
}
