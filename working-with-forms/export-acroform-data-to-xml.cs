using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "formdata.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPdf);

        // Verify that the document contains an AcroForm with fields
        // FieldCollection exposes a Count() method, not a Count property
        if (pdfDoc.Form == null || pdfDoc.Form.Fields == null || pdfDoc.Form.Fields.Count() == 0)
        {
            Console.WriteLine("No AcroForm fields found.");
            return;
        }

        // Create an XML document to hold the form data
        XDocument xmlDoc = new XDocument(new XElement("FormData"));

        // Iterate over each field and write its data to XML
        foreach (Field field in pdfDoc.Form.Fields)
        {
            string name = field.PartialName ?? string.Empty;
            string value = string.Empty;

            // Extract the value based on the concrete field type
            switch (field)
            {
                case TextBoxField txt:
                    value = txt.Value ?? string.Empty;
                    break;
                case CheckboxField chk:
                    value = chk.Checked ? "true" : "false";
                    break;
                case RadioButtonField rad:
                    value = rad.Value ?? string.Empty;
                    break;
                case ListBoxField list:
                    // SelectedItems may be null, guard against it
                    value = list.SelectedItems != null ? string.Join(",", list.SelectedItems) : string.Empty;
                    break;
                case ComboBoxField combo:
                    value = combo.Value ?? string.Empty;
                    break;
                case SignatureField _:
                    // Signature fields do not expose a textual value
                    value = "[Signature]";
                    break;
                default:
                    // Fallback: try to read a generic "Value" property via reflection
                    var prop = field.GetType().GetProperty("Value");
                    if (prop != null)
                    {
                        var propValue = prop.GetValue(field);
                        value = propValue?.ToString() ?? string.Empty;
                    }
                    break;
            }

            XElement fieldElement = new XElement("Field",
                new XAttribute("name", name),
                new XAttribute("type", field.GetType().Name),
                new XAttribute("value", value));

            xmlDoc.Root?.Add(fieldElement);
        }

        // Save the XML file
        xmlDoc.Save(outputXml);
        Console.WriteLine($"AcroForm data exported to '{outputXml}'.");
    }
}
