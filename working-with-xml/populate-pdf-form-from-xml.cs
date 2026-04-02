using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the PDF form template (must contain form fields)
        string templatePath = "template.pdf";
        // Path to the XML file that holds the data records
        string xmlPath = "data.xml";

        // ---------------------------------------------------------------------
        // Validate that the required files exist before attempting to load them.
        // This prevents an unhandled FileNotFoundException at runtime.
        // ---------------------------------------------------------------------
        if (!File.Exists(templatePath))
        {
            Console.WriteLine($"Error: PDF template not found at '{Path.GetFullPath(templatePath)}'.");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"Error: XML data file not found at '{Path.GetFullPath(xmlPath)}'.");
            return;
        }

        // Load the XML document containing one or more <Record> elements
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load XML file: {ex.Message}");
            return;
        }

        XmlNodeList recordNodes = xmlDoc.SelectNodes("//Record");
        if (recordNodes == null || recordNodes.Count == 0)
        {
            Console.WriteLine("No <Record> elements found in the XML file.");
            return;
        }

        int recordIndex = 1;
        foreach (XmlNode recordNode in recordNodes)
        {
            // Open a fresh copy of the template for each record
            using (Document pdfDoc = new Document(templatePath))
            {
                // Iterate over each child element of the <Record> – element name = field name
                foreach (XmlNode fieldNode in recordNode.ChildNodes)
                {
                    // Skip text nodes (e.g., whitespace) that may appear between elements
                    if (fieldNode.NodeType != XmlNodeType.Element)
                        continue;

                    string fieldName = fieldNode.Name;
                    string fieldValue = fieldNode.InnerText ?? string.Empty;

                    // Verify that the PDF actually contains a field with this name
                    if (pdfDoc.Form.HasField(fieldName))
                    {
                        // The Form indexer returns a WidgetAnnotation; cast it to Aspose.Pdf.Forms.Field
                        Field pdfField = pdfDoc.Form[fieldName] as Field;
                        if (pdfField == null)
                            continue; // safety check – not a form field

                        // Most simple text fields are TextBoxField – set its Value directly
                        if (pdfField is TextBoxField textBox)
                        {
                            textBox.Value = fieldValue;
                        }
                        else
                        {
                            // For other field types (e.g., CheckBoxField) attempt to set a generic Value property via reflection
                            var valueProp = pdfField.GetType().GetProperty("Value");
                            if (valueProp != null && valueProp.CanWrite)
                            {
                                valueProp.SetValue(pdfField, fieldValue, null);
                            }
                        }
                    }
                }

                // Save the filled PDF – each record gets its own file
                string outputFileName = $"filled-{recordIndex}.pdf";
                try
                {
                    pdfDoc.Save(outputFileName);
                    Console.WriteLine($"Record {recordIndex} saved to '{outputFileName}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to save PDF for record {recordIndex}: {ex.Message}");
                }
            }

            recordIndex++;
        }
    }
}
