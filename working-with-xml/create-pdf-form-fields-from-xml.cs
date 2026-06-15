using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // Step 1: Create a simple PDF document with a single page.
        // ------------------------------------------------------------
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // ------------------------------------------------------------
        // Step 2: Re‑open the PDF and add form fields defined in XML.
        // ------------------------------------------------------------
        using (Document doc = new Document("input.pdf"))
        {
            // XML that describes the form fields and their default values.
            string xmlContent = @"<Fields>
    <Field Name='FullName' Type='TextBox' DefaultValue='John Doe' />
    <Field Name='Agree'    Type='CheckBox' DefaultValue='true' />
    <Field Name='Age'      Type='TextBox' DefaultValue='30' />
    <Field Name='Subscribe' Type='CheckBox' DefaultValue='false' />
</Fields>";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);

            XmlNodeList fieldNodes = xmlDoc.SelectNodes("/Fields/Field");
            int fieldIndex = 0;
            foreach (XmlNode fieldNode in fieldNodes)
            {
                string fieldName = fieldNode.Attributes["Name"].Value;
                string fieldType = fieldNode.Attributes["Type"].Value;
                string defaultValue = fieldNode.Attributes["DefaultValue"].Value;

                // Simple layout: each field is placed 30 points lower than the previous one.
                double lowerLeftX = 100;
                double lowerLeftY = 700 - (fieldIndex * 30);
                double upperRightX = 300;
                double upperRightY = lowerLeftY + 20;
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(lowerLeftX, lowerLeftY, upperRightX, upperRightY);

                if (fieldType == "TextBox")
                {
                    TextBoxField textBox = new TextBoxField(doc.Pages[1], rect);
                    textBox.PartialName = fieldName;
                    textBox.Value = defaultValue;
                    doc.Form.Add(textBox);
                }
                else if (fieldType == "CheckBox")
                {
                    CheckboxField checkBox = new CheckboxField(doc.Pages[1], rect);
                    checkBox.PartialName = fieldName;
                    bool isChecked = false;
                    if (Boolean.TryParse(defaultValue, out isChecked))
                    {
                        checkBox.Checked = isChecked;
                    }
                    doc.Form.Add(checkBox);
                }

                fieldIndex++;
            }

            // Save the resulting PDF with the newly added form fields.
            doc.Save("output.pdf");
        }
    }
}
