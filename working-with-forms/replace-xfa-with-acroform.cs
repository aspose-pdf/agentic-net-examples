using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ReplaceXfaWithAcroForm
{
    static void Main()
    {
        const string inputPdfPath = "input_with_xfa.pdf";
        const string xmlTemplatePath = "fields_template.xml";
        const string outputPdfPath = "output_acroform.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlTemplatePath))
        {
            Console.Error.WriteLine($"XML template not found: {xmlTemplatePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Check if the document contains an XFA form
            if (pdfDoc.Form.HasXfa)
            {
                Console.WriteLine("Document contains XFA form. Converting to AcroForm...");

                // Load the XML template that defines the fields
                XmlDocument fieldTemplate = new XmlDocument();
                fieldTemplate.Load(xmlTemplatePath);

                // Example: assume the template contains <field name="FieldName" type="TextBox" />
                XmlNodeList fieldNodes = fieldTemplate.SelectNodes("//field");
                if (fieldNodes != null)
                {
                    foreach (XmlNode fieldNode in fieldNodes)
                    {
                        string fieldName = fieldNode.Attributes["name"]?.Value;
                        string fieldType = fieldNode.Attributes["type"]?.Value?.ToLowerInvariant();

                        if (string.IsNullOrEmpty(fieldName))
                            continue; // skip malformed entries

                        // For this demo we only handle TextBox fields.
                        // The TextBoxField constructor takes (Page, Rectangle).
                        // The field name is assigned via the PartialName property.
                        TextBoxField txtField = new TextBoxField(pdfDoc.Pages[1], new Rectangle(100, 700, 300, 720));
                        txtField.PartialName = fieldName;
                        txtField.Value = string.Empty; // default empty value

                        // Add the field to the document's AcroForm collection
                        pdfDoc.Form.Add(txtField);
                    }
                }

                // Optionally, remove the XFA data to avoid conflicts.
                // Setting the form type to Standard forces the document to be treated as an AcroForm.
                pdfDoc.Form.Type = FormType.Standard;
                // The XFA property is read‑only; after changing the type, the XFA data is ignored during saving.
            }
            else
            {
                Console.WriteLine("Document does not contain an XFA form. No conversion needed.");
            }

            // Save the modified PDF (lifecycle rule: use save)
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"AcroForm PDF saved to '{outputPdfPath}'.");
        }
    }
}
