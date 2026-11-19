using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for DefaultAppearance
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text; // for FontRepository

class Program
{
    static void Main()
    {
        const string templatePdfPath = "template.pdf";   // existing blank PDF or template
        const string xmlDefinitionPath = "fields.xml";   // XML that defines fields and defaults
        const string outputPdfPath = "filled_form.pdf";

        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }

        if (!File.Exists(xmlDefinitionPath))
        {
            Console.Error.WriteLine($"XML definition not found: {xmlDefinitionPath}");
            return;
        }

        // Load the XML that describes the form fields
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlDefinitionPath);

        // Open the PDF inside a using block (lifecycle rule)
        using (Document pdfDoc = new Document(templatePdfPath))
        {
            // Ensure we have at least one page to host the fields
            // Aspose.Pdf uses 1‑based page indexing (global rule)
            Page firstPage = pdfDoc.Pages[1];

            // Simple rectangle that will be used for every generated field.
            // In a real scenario you would read coordinates from the XML.
            const float left = 100f;
            const float bottom = 600f;
            const float width = 200f;
            const float height = 20f;
            var defaultRect = new Aspose.Pdf.Rectangle(left, bottom, left + width, bottom + height);

            // Iterate over each <Field> element in the XML
            XmlNodeList fieldNodes = xmlDoc.SelectNodes("//Field");
            foreach (XmlNode fieldNode in fieldNodes)
            {
                // Read required attributes
                string type = fieldNode.Attributes["type"]?.Value?.Trim();
                string name = fieldNode.Attributes["name"]?.Value?.Trim();
                string value = fieldNode.Attributes["value"]?.Value?.Trim() ?? string.Empty;

                if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(name))
                    continue; // skip malformed entries

                switch (type.ToLowerInvariant())
                {
                    case "textbox":
                        // TextBoxField constructor expects a rectangle; set the field name via PartialName
                        var txtField = new TextBoxField(firstPage, defaultRect);
                        txtField.PartialName = name;
                        txtField.Value = value;
                        // Appearance – use DefaultAppearance (TextState does not exist)
                        txtField.DefaultAppearance = new DefaultAppearance(
                            FontRepository.FindFont("Helvetica"), 12, System.Drawing.Color.Black);
                        pdfDoc.Form.Add(txtField, 1);
                        break;

                    case "checkbox":
                        var chkField = new CheckboxField(firstPage, defaultRect);
                        chkField.PartialName = name;
                        if (bool.TryParse(value, out bool isChecked))
                            chkField.Checked = isChecked;
                        else
                            chkField.Checked = false;
                        // Appearance
                        chkField.DefaultAppearance = new DefaultAppearance(
                            FontRepository.FindFont("Helvetica"), 12, System.Drawing.Color.Black);
                        pdfDoc.Form.Add(chkField, 1);
                        break;

                    case "radiobutton":
                        // RadioButtonField represents the whole group. We create a group per XML entry.
                        string groupName = fieldNode.Attributes["group"]?.Value?.Trim() ?? name;
                        var radioGroup = new RadioButtonField(firstPage);
                        radioGroup.PartialName = groupName;
                        // Add a single option – in a real scenario you would have multiple <Option> nodes.
                        radioGroup.AddOption(name, defaultRect);
                        // Select the option if value == "true"
                        if (bool.TryParse(value, out bool isSelected) && isSelected)
                            radioGroup.Selected = 1; // 1‑based index of the first (and only) option
                        pdfDoc.Form.Add(radioGroup, 1);
                        break;

                    case "listbox":
                        var listField = new ListBoxField(firstPage, defaultRect);
                        listField.PartialName = name;
                        // Expect child <Item> elements for list entries
                        XmlNodeList items = fieldNode.SelectNodes("Item");
                        foreach (XmlNode itemNode in items)
                        {
                            string itemText = itemNode.InnerText.Trim();
                            // AddOption(exportValue, displayName) – we use the same string for both
                            listField.AddOption(itemText, itemText);
                        }
                        // Default selected index (optional attribute "selectedIndex") – 1‑based
                        if (int.TryParse(fieldNode.Attributes["selectedIndex"]?.Value, out int selectedIdx) && selectedIdx > 0)
                        {
                            listField.Selected = selectedIdx; // single selection
                        }
                        pdfDoc.Form.Add(listField, 1);
                        break;

                    default:
                        Console.WriteLine($"Unsupported field type: {type}");
                        break;
                }
            }

            // Save the resulting PDF (save rule – no extra SaveOptions needed for PDF)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form PDF created: {outputPdfPath}");
    }
}
