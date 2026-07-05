using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // PDF with form fields
        const string xmlPath   = "data.xml";       // XML containing a subset of field values
        const string outputPdf = "output.pdf";

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

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Load the XML document
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // Iterate over each element that represents a form field.
            // Assume the XML structure is <Fields><Field name="FieldName">Value</Field>...</Fields>
            XmlNodeList? fieldNodes = xmlDoc.SelectNodes("//Field[@name]");
            if (fieldNodes != null)
            {
                foreach (XmlNode node in fieldNodes)
                {
                    string? fieldName = node.Attributes?["name"]?.Value;
                    string fieldValue = node.InnerText ?? string.Empty;

                    if (string.IsNullOrEmpty(fieldName))
                        continue; // skip malformed entries

                    // Check if the PDF contains a field with this name
                    if (pdfDoc.Form.HasField(fieldName))
                    {
                        // The Form indexer returns a WidgetAnnotation; cast it to Field safely.
                        Field? pdfField = pdfDoc.Form[fieldName] as Field;
                        if (pdfField != null)
                        {
                            pdfField.Value = fieldValue;
                        }
                    }
                }
            }

            // Save the updated PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPdf}'.");
    }
}
