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

        // Load the source PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Clear existing XFA form (if any)
            // -----------------------------------------------------------------
            if (pdfDoc.Form.HasXfa)
            {
                // Assign an empty XFA document to remove the dynamic XFA content
                XmlDocument emptyXfa = new XmlDocument();
                emptyXfa.LoadXml("<xfa></xfa>");
                pdfDoc.Form.AssignXfa(emptyXfa);
            }

            // -----------------------------------------------------------------
            // 2. Load field definitions from the external XML template
            // -----------------------------------------------------------------
            XmlDocument fieldDefDoc = new XmlDocument();
            fieldDefDoc.Load(xmlTemplatePath);
            XmlNodeList fieldNodes = fieldDefDoc.SelectNodes("//field");

            if (fieldNodes != null)
            {
                foreach (XmlNode node in fieldNodes)
                {
                    // -----------------------------------------------------------------
                    // Basic attribute extraction with null‑safety
                    // -----------------------------------------------------------------
                    string name = node.Attributes?["name"]?.Value;
                    string type = node.Attributes?["type"]?.Value;
                    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(type))
                        continue; // skip malformed entries

                    int pageNumber = 1;
                    int.TryParse(node.Attributes?["page"]?.Value, out pageNumber);
                    if (pageNumber < 1) pageNumber = 1;

                    // Rectangle coordinates (llx, lly, urx, ury) – default to 0 if parsing fails
                    double llx = 0, lly = 0, urx = 0, ury = 0;
                    double.TryParse(node.Attributes?["llx"]?.Value, out llx);
                    double.TryParse(node.Attributes?["lly"]?.Value, out lly);
                    double.TryParse(node.Attributes?["urx"]?.Value, out urx);
                    double.TryParse(node.Attributes?["ury"]?.Value, out ury);
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                    // Ensure the target page exists
                    if (pageNumber > pdfDoc.Pages.Count)
                        continue; // skip invalid page reference

                    Page targetPage = pdfDoc.Pages[pageNumber];

                    // -----------------------------------------------------------------
                    // 3. Create corresponding AcroForm field based on the type attribute
                    // -----------------------------------------------------------------
                    switch (type.ToLowerInvariant())
                    {
                        case "textbox":
                            TextBoxField txtField = new TextBoxField(targetPage, rect);
                            txtField.PartialName = name;
                            pdfDoc.Form.Add(txtField);
                            break;

                        case "checkbox":
                            CheckboxField chkField = new CheckboxField(targetPage, rect);
                            chkField.PartialName = name;
                            pdfDoc.Form.Add(chkField);
                            break;

                        case "radiobutton":
                            // RadioButtonField does not have a (Page, Rectangle) ctor – use the page‑only ctor and set Rect manually
                            RadioButtonField radField = new RadioButtonField(targetPage);
                            radField.PartialName = name;
                            radField.Rect = rect;
                            pdfDoc.Form.Add(radField);
                            break;

                        case "listbox":
                            ListBoxField listField = new ListBoxField(targetPage, rect);
                            listField.PartialName = name;
                            pdfDoc.Form.Add(listField);
                            break;

                        case "combobox":
                            ComboBoxField comboField = new ComboBoxField(targetPage, rect);
                            comboField.PartialName = name;
                            pdfDoc.Form.Add(comboField);
                            break;

                        // Add more field types as needed
                        default:
                            Console.WriteLine($"Unsupported field type '{type}' for field '{name}'.");
                            break;
                    }
                }
            }

            // -----------------------------------------------------------------
            // 4. Save the modified PDF (AcroForm only)
            // -----------------------------------------------------------------
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with AcroForm saved to '{outputPdfPath}'.");
    }
}
