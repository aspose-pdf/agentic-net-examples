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

        // Load the source PDF (XFA form)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Verify that the document contains an XFA form
            if (pdfDoc.Form.HasXfa)
            {
                // Switch the form type to Standard (AcroForm)
                pdfDoc.Form.Type = FormType.Standard;
            }

            // Load the XML template that defines the AcroForm fields
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlTemplatePath);

            // Example XML structure:
            // <Fields>
            //   <Field name="FirstName" type="TextBox" x="100" y="700" width="200" height="20"/>
            //   <Field name="Agree" type="CheckBox" x="100" y="650" width="15" height="15"/>
            // </Fields>

            XmlNodeList fieldNodes = xmlDoc.SelectNodes("//Field");
            if (fieldNodes != null)
            {
                foreach (XmlNode fieldNode in fieldNodes)
                {
                    // ----- Safe extraction of attributes -----
                    string fieldName = fieldNode.Attributes?["name"]?.Value;
                    string fieldType = fieldNode.Attributes?["type"]?.Value?.ToLowerInvariant();
                    if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(fieldType))
                    {
                        Console.WriteLine("Field definition is missing required attributes. Skipping.");
                        continue;
                    }

                    // Parse numeric attributes with defaults and validation
                    bool okX = double.TryParse(fieldNode.Attributes?["x"]?.Value, out double llx);
                    bool okY = double.TryParse(fieldNode.Attributes?["y"]?.Value, out double lly);
                    bool okW = double.TryParse(fieldNode.Attributes?["width"]?.Value, out double width);
                    bool okH = double.TryParse(fieldNode.Attributes?["height"]?.Value, out double height);

                    // Apply sensible defaults when parsing fails
                    if (!okX) llx = 0;
                    if (!okY) lly = 0;
                    if (!okW) width = 100;
                    if (!okH) height = 20;

                    double urx = llx + width;
                    double ury = lly + height;
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                    // ----- Create field based on type -----
                    switch (fieldType)
                    {
                        case "textbox":
                            TextBoxField txtField = new TextBoxField(pdfDoc.Pages[1], rect);
                            txtField.PartialName = fieldName;
                            pdfDoc.Form.Add(txtField);
                            break;

                        case "checkbox":
                            CheckboxField chkField = new CheckboxField(pdfDoc.Pages[1], rect);
                            chkField.PartialName = fieldName;
                            pdfDoc.Form.Add(chkField);
                            break;

                        case "radiobutton":
                            // RadioButtonField constructor takes only a Page (per Aspose.PDF API).
                            RadioButtonField radField = new RadioButtonField(pdfDoc.Pages[1]);
                            radField.PartialName = fieldName;
                            radField.Rect = rect; // set the rectangle explicitly
                            pdfDoc.Form.Add(radField);
                            break;

                        case "listbox":
                            ListBoxField listField = new ListBoxField(pdfDoc.Pages[1], rect);
                            listField.PartialName = fieldName;
                            pdfDoc.Form.Add(listField);
                            break;

                        case "combobox":
                            ComboBoxField comboField = new ComboBoxField(pdfDoc.Pages[1], rect);
                            comboField.PartialName = fieldName;
                            pdfDoc.Form.Add(comboField);
                            break;

                        case "signature":
                            SignatureField sigField = new SignatureField(pdfDoc.Pages[1], rect);
                            sigField.PartialName = fieldName;
                            pdfDoc.Form.Add(sigField);
                            break;

                        default:
                            Console.WriteLine($"Unsupported field type '{fieldType}' for field '{fieldName}'. Skipping.");
                            break;
                    }
                }
            }

            // Save the modified PDF with AcroForm fields
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"AcroForm PDF saved to '{outputPdfPath}'.");
        }
    }
}
